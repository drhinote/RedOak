﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forgot : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PasswordRecovery1.MailDefinition.BodyFileName = "~/search/TextFile.txt";
    }
}