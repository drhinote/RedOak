<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Debug="true" Inherits="search_Default" EnableViewStateMac="False" %>
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
		.auto-style1 {
			width: 150px;
		}
		.auto-style2 {
			width: 78px;
		}
	</style>
	<script type="text/javascript"> 
		function GetUtcOffset() {
			var date = new Date();
			return (-date.getTimezoneOffset()).toString();
		}

		function PostWithLocalTime() {
			window.location.replace('/search/search.aspx?utcoffset=' + GetUtcOffset());
		}

		window.onload = function () {
			if (<%=GotUtcOffset%> == 0) {
				PostWithLocalTime();
			}
		}
	</script>
</head>

<body>
	<img alt="" height="79" src="/wp5082f3a7_06.png" style="position:absolute; left: 314px; top: 10px;" width="81" />
	<div style="position: absolute; left: 35px; top: 24px; width: 423px; height: 71px;">
		<div style="margin: 0px; text-align: left; font-weight: 400;">
			<span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC<br /></span>
			<span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-weight: 700; font-size: 19px; line-height: 1.21em;">Data Retrieval Portal</span>
		</div>
	</div>
	<form id="form1" runat="server" defaultbutton="Search">
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
	 &nbsp;|  <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/management/ChangePassword.aspx">Change Password</asp:HyperLink></div>
	
	<br />
		<br />
		<br />
		<br />

		<br /><br /><a href="https://admin.redoakinstruments.com">Admin Portal</a><br /><br />
		Search for tests by entering some information in the boxes below and/or selecting a date range.&nbsp;
		<br />
		Searches will match partial input (i.e. a last name only in the Name box) and any letter case.&nbsp;
		<br />
		<br />
		<br />
		<br />
		<div>

		<table>
			<tr>
				<td class="auto-style1" style="text-align: center">From:</td>
				<td class="auto-style2" style="text-align: center">To:</td>
			</tr>
			<tr>
				<td rowspan="6">
					<asp:Calendar ID="Calendar1" runat="server" Height="200px" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" ShowGridLines="True" Width="220px">
						<DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
						<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
						<OtherMonthDayStyle ForeColor="#CC9966" />
						<SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
						<SelectorStyle BackColor="#FFCC66" />
						<TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
						<TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
					</asp:Calendar>
				</td>
				<td rowspan="6">
					<asp:Calendar ID="Calendar2" runat="server" Height="200px" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" ShowGridLines="True" Width="220px">
						<DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
						<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
						<OtherMonthDayStyle ForeColor="#CC9966" />
						<SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
						<SelectorStyle BackColor="#FFCC66" />
						<TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
						<TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
					</asp:Calendar>
				</td>
				<td>
					<h3><label runat="server" visible="false" id="lblLoading">Generating report, please wait...</label></h3>
				</td>
			</tr>
		</table>
			

			
		</div>
		<table>
		<tr>
			<td>DOB:</td><td><asp:TextBox ID="dob" runat="server" Width="135px"></asp:TextBox></td>
			</tr>
		<tr>
			<td>Social:</td><td><asp:TextBox ID="social" runat="server" Width="135px"></asp:TextBox></td>
			</tr>
		<tr>
			<td>Optional ID:</td><td><asp:TextBox ID="opid" runat="server" Width="135px"></asp:TextBox></td>
			</tr>
		<tr>
			<td>UUID or Machine:</td><td><asp:TextBox ID="uuid" runat="server" Width="135px"></asp:TextBox></td>
		</tr>
			<tr>
				<td> 
					<asp:Button ID="Button1" runat="server" OnClick="Clear_Click" Text="Clear" />
				</td>
				<td>
					<asp:Button ID="Search" runat="server" Text="Search" Width="139px" OnClick="Search_Click" />
				</td>
			</tr>
		</table>
					<asp:Button ID="showAll" runat="server" Text="Show All" OnClick="showAll_Click" />
				<br />
		<br />
		Results:<br />
		<asp:Label ID="Error" runat="server"></asp:Label>
		<asp:Table ID="Table1" runat="server">
		</asp:Table>
	<br/><br/><br/><br/><br/><br/><br/><br/><br/>
	</form>
	</body>
</html>
