<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgot.aspx.cs" Inherits="Forgot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server" profile="http://www.w3.org/2005/10/profile">
<link rel="icon" href="https://www.redoakreports.com/myicon.ico" />
    <title>Red Oak Instruments, LLC.</title>
    <style>
        body {
            background-color: transparent;
            font-family: Arial;
            font-size: 14px;
            font-weight: 500;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
     <img alt="" height="79" src="wp5082f3a7_06.png" style="position:absolute; left: 314px; top: 10px;" width="81" />
     <div style="position: absolute; left: 35px; top: 24px; width: 423px; height: 71px;">
        <div style="margin: 0px; text-align: left; font-weight: 400;">
            <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC<br /></span>
            <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0);font-weight: 700; font-size: 19px; line-height: 1.21em;">Data Retrieval Portal</span>
        </div>
     </div>
     <div style="position:absolute; left: 40px; top:120px; width:90%;">
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx" style="font-size: medium">Home</asp:HyperLink>
         <br />
         <br />
         <asp:PasswordRecovery ID="PasswordRecovery1" runat="server">
         </asp:PasswordRecovery>
     </div>
    </form>
   <div style="position: relative; text-align: center"><p style="position: fixed; bottom: 0; width:100%;">
      <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=dANzVqdwMYTVdx2QrCPjCH3D214dwl3SbgNu7qnzxc54s3dhMOmP5J6fQMY"></script></span></p></div>
</body>
</html>
