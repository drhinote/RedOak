using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class error_MissingDataError : System.Web.UI.Page
{
	private string uuid;
	private string time;

	protected void Page_Load(object sender, EventArgs e)
	{
		uuid = Request.QueryString.Get("uuid") != null ? Request.QueryString.Get("uuid") : "null";
		time = Request.QueryString.Get("time") != null ? Request.QueryString.Get("time") : "null";
	}

	public string Uuid { get { return uuid; } }
	public string Time { get { return time; } }
}