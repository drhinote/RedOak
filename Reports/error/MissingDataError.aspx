<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissingDataError.aspx.cs" Inherits="error_MissingDataError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="MissingDataError" runat="server" style="width:1400px; margin: 10% 10%">
    <div>
        <div><h1>There was an error retrieving the data for this report.</h1></div>
        <div><h2>A report with UUID=<%=Uuid%> and Time=<%=Time%> doesn't appear to be in the database.</h2></div>
    </div>
    </form>
</body>
</html>
