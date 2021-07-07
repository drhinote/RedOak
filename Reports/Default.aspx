<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" profile="http://www.w3.org/2005/10/profile">
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
     <div style="text-align: right">
         <span style="font-style: italic">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                Not signed in, please click the &#39;Login&#39; link to sign in...
            </AnonymousTemplate>
            <LoggedInTemplate>
                Logged in as
                <asp:LoginName ID="LoginName1" runat="server" />
            </LoggedInTemplate>
        </asp:LoginView><br />
             </span>
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
     </div>
     <img alt="" height="79" src="wp5082f3a7_06.png" style="position:absolute; left: 314px; top: 10px;" width="81" />
     <div style="position: absolute; left: 35px; top: 24px; width: 423px; height: 71px;">
        <div style="margin: 0px; text-align: left; font-weight: 400;">
            <span style="font-family: 'Times New Roman'; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC<br /></span>
            <span style="font-family: 'Times New Roman'; color: rgb(140, 0, 0);font-weight: 700; font-size: 19px; line-height: 1.21em;">Data Retrieval Portal</span>
        </div>
     </div>
     <div style="position:absolute; left: 40px; top:120px; width:90%;">

         <h2><span style="font-family: 'Times New Roman'; font-weight: 700; font-size: 21px; line-height: 1.43em;">Welcome!</span></h2>
         <asp:LoginView ID="LoginView2" runat="server">
            <AnonymousTemplate>
                If you are registered and have a user ID and password, continue to the
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/search/Search.aspx">Search Page >></asp:HyperLink><br />
                <br />
                User ID&#39;s and password must be created using the software provided in your handset Kit.<br />
            </AnonymousTemplate>
            <LoggedInTemplate>
                Please continue to the
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/search/Search.aspx">Search Page >></asp:HyperLink><br />
                <br /><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/search/ChangePassword.aspx">Change my password >></asp:HyperLink>
            </LoggedInTemplate>
        </asp:LoginView>
     </div>
   </form>
   <div style="position: relative; text-align: center"><p style="position: fixed; bottom: 0; width:100%;">
      <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=dANzVqdwMYTVdx2QrCPjCH3D214dwl3SbgNu7qnzxc54s3dhMOmP5J6fQMY"></script></span></p></div>
  </body>
</html>
