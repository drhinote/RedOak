using System;
using System.Collections.Generic;
using System.Linq;using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class search_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }
    protected void ContinuePushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx");
    }
}