using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using Roi.Data.Models;
using Roi.Data;
using System.Web;
using System.Text;

public class SearchResult
{
    public string Name { get; set; }
    public string DoB { get; set; }
    public string Social { get; set; }
    public string OpId { get; set; }
    public string UUID { get; set; }
    public string Tester { get; set; }
    public string Date { get; set; }
    public string Company { get; set; }
}

public partial class search_Default : System.Web.UI.Page
{
    string userName;
    int UtcOffset { get; set; }
    public int GotUtcOffset { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        userName = HttpContext.Current.User.Identity.Name;
        var utcOffset = Request.QueryString.Get("utcoffset");
        if (utcOffset != null)
        {
            GotUtcOffset = 1;
            UtcOffset = Convert.ToInt32(utcOffset);
        }
    }

    private string testDateTime(long? testTimeMiliseconds)
    {
        DateTime testDate = new DateTime(1970, 1, 1, 0, 0, 0);
        testDate = testDate.AddMilliseconds(Convert.ToDouble(testTimeMiliseconds));
        testDate = testDate.AddMinutes(UtcOffset);
        return testDate.ToString("MM/dd/yyyy hh:mm tt");
    }

    private void addRow(Guid companyId, long? time, string dob, string opid, string tester, string uuid, Guid id)
    {
        TableRow tr = new TableRow();
        TableCell tc;

        // Id
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = time.ToString();
        tr.Cells.Add(tc);

        // Dob
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = dob;
        tr.Cells.Add(tc);

        // UuId
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = uuid;
        tr.Cells.Add(tc);

        // OpId
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = opid;
        tr.Cells.Add(tc);

        // Time
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = testDateTime(time);
        tr.Cells.Add(tc);

        // Tester
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = tester;
        tr.Cells.Add(tc);

        // report
        tc = new TableCell();
        tc.Width = 150;
        tc.Text = "<a href=\"/reports/Full.aspx?type=full&id=" + id.ToString() + "&companyId=" + companyId.ToString() + "&utcoffset=" + UtcOffset + "\">View Full Report</a>";
        tr.Cells.Add(tc);

        tc = new TableCell();
        tc.Width = 150;
        tc.Text = "<a href=\"/reports/Full.aspx?type=onepage&id=" + id.ToString() + "&companyId=" + companyId.ToString() + "&utcoffset=" + UtcOffset + "\">View 1-Page Report</a>";
        tr.Cells.Add(tc);

        Table1.Rows.Add(tr);
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        PerformSearch();
    }

    private void PerformSearch()
    {
        var search = new TestSearch(dob.Text, social.Text, opid.Text, uuid.Text, Calendar1.SelectedDate, Calendar2.SelectedDate);

        
        using (var ctx = new RoiDb())
        {
            if (userName == "wcp") {
                foreach (var res in ctx.Tests.Include("Tester")
                   .Where(t => (search.Dob != "") ? t.Dob.Contains(search.Dob) : true)
                   .Where(t => (search.Opid != "") ? t.OpId.Contains(search.Opid) : true)
                   .Where(t => (search.Uuid != "") ? t.UuId.Contains(search.Uuid) : true)
                   .Where(t => (search.StartDate != DateTime.MinValue) ? t.DateTime >= search.StartDate : true)
                   .Where(t => (search.EndDate != DateTime.MinValue) ? t.DateTime <= search.EndDate : true).OrderByDescending(t => t.UnixTimeStamp)
                   .ThenByDescending(t => t.UuId))
                {
                    addRow(res.CompanyId, res.UnixTimeStamp, res.Dob, res.OpId, res.Tester.Name, res.UuId, res.Id);
                }
            } else {
                var companies = ctx.Testers.FirstOrDefault(id => id.Name == userName).Companies.Select(c => c.Id);

                /// TODO: I don't think the calendar search is going to work like this
                /// startTime and endTime need to be set to the calendar times
                /// they've been set by the user

                foreach (var res in ctx.Tests.Include("Tester")
                    .Where(t => companies.Contains(t.CompanyId))
                    .Where(t => (search.Dob != "") ? t.Dob.Contains(search.Dob) : true)
                    .Where(t => (search.Opid != "") ? t.OpId.Contains(search.Opid) : true)
                    .Where(t => (search.Uuid != "") ? t.UuId.Contains(search.Uuid) : true)
                    .Where(t => (search.StartDate != DateTime.MinValue) ? t.DateTime >= search.StartDate : true)
                    .Where(t => (search.EndDate != DateTime.MinValue) ? t.DateTime <= search.EndDate : true).OrderByDescending(t => t.UnixTimeStamp)
                    .ThenByDescending(t => t.UuId)) {
                    addRow(res.CompanyId, res.UnixTimeStamp, res.Dob, res.OpId, res.Tester.Name, res.UuId, res.Id);
                }
            }
        }
    }

    private TableRow BlankRow()
    {
        var tr = new TableRow();
        AddCell("-", tr);
        AddCell("-", tr);
        AddCell("-", tr);
        AddCell("-", tr);
        AddCell("-", tr);
        return tr;
    }

    private void AddCell(string value, TableRow row)
    {
        TableCell tc = new TableCell();
        tc.Text = value;
        tc.Width = 150;
        row.Cells.Add(tc);
    }

    protected void showAll_Click(object sender, EventArgs e)
    {
        Clear_Click(sender, e);
        Search_Click(sender, e);
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        dob.Text = "";
        social.Text = "";
        opid.Text = "";
        uuid.Text = "";
        Calendar1.VisibleDate = DateTime.Today;
        Calendar2.VisibleDate = DateTime.Today;
        Calendar1.SelectedDates.Clear();
        Calendar2.SelectedDates.Clear();
    }
}