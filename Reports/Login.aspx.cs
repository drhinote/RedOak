using Newtonsoft.Json;
using Roi.Data;
using Roi.Data.Models;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            using (var ctx = new RoiDb())
            {
                var user = ctx.Testers.FirstOrDefault(u => u.Name == Login1.UserName && u.Active);
                e.Authenticated = user != null && user.TestPassword(Login1.Password);
             }
        }
        catch (Exception ex)
        {
            output.Text = "Login error: " + ex.Message;
        }
    }
}