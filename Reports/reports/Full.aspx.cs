using Newtonsoft.Json;
using Roi.Data;
using Roi.Data.BusinessLogic;
using Roi.Data.Models;
using Roi.Logic;
using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web;

public partial class reports_Full : System.Web.UI.Page
{
    private ReportData _data;

    protected void Page_Load(object sender, EventArgs e)
    {
        var success = true;
        var id = Request.QueryString.Get("id");
        var companyId = Request.QueryString.Get("companyId");
        var type = Request.QueryString.Get("type");
        var utcOffset = Request.QueryString.Get("utcoffset");


        try
        {
            var task = AnalysisLogic.RetrieveReport(id, companyId);
            task.Wait();
            _data = task.Result.ReportData;
            InjuryBox.BackColor = InjuryColor(_data.InjuryFlag);
            InjuryBox.Text = InjuryText(_data.InjuryFlag);

            FullReport.Visible = (type == "full") ? true : false;

            test_history_chart.Visible = (_data.TotalTests > 1) ? true : false;
        }
        catch (Exception er)
        {
            Roi.Data.Error.LogError(er);
            success = false;
        }

        if (!success)
        {
            Response.StatusCode = 404;
            Page.Response.ContentType = "text/html";
            Page.Response.Redirect("/error/MissingDataError.aspx?uuid=" + id + "&time=0");
        }

        header1.InnerText = header2.InnerText = _data.Company;

    }

    public ReportData Data { get { return _data; } }

    //public string FatigueVariance(double value)
    //{
    //	if (value > 2.01) return "#ff0000";
    //	if (value > 1.11) return "#ffff00";
    //	return "#00e600";
    //}

    //public string FatigueVariance(decimal? value)
    //{
    //	if (value > (decimal?)2.01) return "#ff0000";
    //	if (value > (decimal?)1.11) return "#ffff00";
    //	return "#00e600";
    //}

    public System.Drawing.Color InjuryColor(double value)
    {
        if (value > 89) return System.Drawing.Color.LightGreen;
        if (value > 79) return System.Drawing.Color.Yellow;
        return System.Drawing.Color.Red;
    }

    public string InjuryColorString(double value)
    {
        // remove scale
        value = 100 - (value * 10);

        if (value > 89) return "lightGreen";
        if (value > 79) return "yellow";
        return "red";
    }

    public string InjuryText(double value)
    {
        if (value > 89) return "Normal Results";
        if (value > 79) return "Possible Injury Present";
        return "Evidence of Injury Present";
    }

    public string PercentColor(double value)
    {
        if (value >= .9) return "#00e600";
        if (value >= .8) return "#ffff00";
        return "#ff0000";
    }

    public string PercentColor(decimal? value)
    {
        if (value >= (decimal?).9) return "#00e600";
        if (value >= (decimal?).8) return "#ffff00";
        return "#ff0000";
    }
}

public static class extension
{
    public static string RemoveTime(this string UuidTime)
    {
        var uuid = UuidTime.Substring(0, UuidTime.LastIndexOf('-'));
        return uuid;
    }
}