<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="search_ChangePassword" %>

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
        <div style="text-align: right;">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                Not signed in, please click the &#39;Login&#39; link to sign in...
            </AnonymousTemplate>
            <LoggedInTemplate>
                Logged in as
                <asp:LoginName ID="LoginName1" runat="server" />
            </LoggedInTemplate>
        </asp:LoginView><br />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
     </div>
     <img alt="" height="79" src="/wp5082f3a7_06.png" style="position:absolute; left: 314px; top: 10px;" width="81" />
     <div style="position: absolute; left: 35px; top: 24px; width: 423px; height: 71px;">
        <div style="margin: 0px; text-align: left; font-weight: 400;">
            <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC<br /></span>
            <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0);font-weight: 700; font-size: 19px; line-height: 1.21em;">Data Retrieval Portal</span>
        </div>
     </div>
     <div style="position:absolute; left: 40px; top:120px; width:90%;">
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/search/Search.aspx" style="font-size: medium">Back to Search</asp:HyperLink>

         <br />
         <br />
         <asp:ChangePassword ID="ChangePassword1" runat="server">
             <ChangePasswordTemplate>
                 <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                     <tr>
                         <td>
                             <table cellpadding="0">
                                 <tr>
                                     <td align="center" colspan="2">Change Your Password</td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Password:</asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" ErrorMessage="New Password is required." ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
                                     </td>
                                     <td>
                                         <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" ErrorMessage="Confirm New Password is required." ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="center" colspan="2">
                                         <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry." ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="center" colspan="2" style="color:Red;">
                                         <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                     </td>
                                 </tr>
                                 <tr>
                                     <td align="right">
                                         <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" ValidationGroup="ChangePassword1" />
                                     </td>
                                     <td>
                                         <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" OnClick="CancelPushButton_Click" Text="Cancel" />
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
             </ChangePasswordTemplate>
             <SuccessTemplate>
                 <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                     <tr>
                         <td>
                             <table cellpadding="0">
                                 <tr>
                                     <td align="center" colspan="2">Change Password Complete</td>
                                 </tr>
                                 <tr>
                                     <td>Your password has been changed!</td>
                                 </tr>
                                 <tr>
                                     <td align="right" colspan="2">
                                         <asp:Button ID="ContinuePushButton" runat="server" CausesValidation="False" CommandName="Continue" OnClick="ContinuePushButton_Click" Text="Continue" />
                                     </td>
                                 </tr>
                             </table>
                         </td>
                     </tr>
                 </table>
             </SuccessTemplate>
         </asp:ChangePassword>

     </div>
    </form>
   <div style="position: relative; text-align: center"><p style="position: fixed; bottom: 0; width:100%;">
      <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=dANzVqdwMYTVdx2QrCPjCH3D214dwl3SbgNu7qnzxc54s3dhMOmP5J6fQMY"></script></span></p></div>
</body>
</html>