<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="menu.aspx.cs" Inherits="RedoakAdmin.menu" %>

 <asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     <!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Red Oak Instruments, LLC.</title>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />

    <!-- Angular Material requires Angular.js Libraries -->
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-animate.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-aria.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-messages.min.js"></script>

    <!-- Angular Material Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.js"></script>

    <script type="text/javascript" src="../Scripts/Angular.js"></script>

</head>
    
<body>
    <br />
    <br />
    <br />
    <div style="margin: 10px; text-align: left; font-weight: 400;">
        <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC <img alt="" height="79" src="wp5082f3a7_06.png" width="81" /> </span>
    </div>
    
    <div ng-controller="AppCtrl as appCtrl" ng-cloak="" ng-app="ROI" class="gridListdemoBasicUsage"
            style="width:100%; height:100%">
        <md-grid-list md-cols-xs="1" md-cols-sm="2" md-cols-md="2" md-cols-gt-md="4"
                        md-row-height-gt-md="1:1" md-row-height="1:1"
                        md-gutter="12px" md-gutter-gt-sm="8px">

            <%--<md-grid-tile class="green">
                <md-button href="setup.aspx" class="md-raised"> <h4>Guided Setup</h4>  </md-button>
            </md-grid-tile>--%>

            <md-grid-tile class="green">
                <md-button href="companies/companies.aspx" class="md-raised"> <h4>Manage Companies</h4>  </md-button>
            </md-grid-tile>

            <md-grid-tile class="yellow">
                <md-button href="devices/devices.aspx" class="md-raised"> <h4>Manage Devices</h4> </md-button>
            </md-grid-tile>

            <md-grid-tile class="green">
                <md-button href="users/users.aspx" class="md-raised"> <h4>Manage Users</h4> </md-button>
            </md-grid-tile>

            <md-grid-tile class="yellow">
                <md-button href="https://reports.redoakinstruments.com" class="md-raised"> <h4>View Reports</h4> </md-button>
            </md-grid-tile>

            <md-grid-tile class="red">
                <md-button href="registration/register.aspx" class="md-raised"> <h4>Register Device</h4> </md-button>
            </md-grid-tile>

            <md-grid-tile class="red">
                <md-button href="download.html" class="md-raised"> <h4>Downloads</h4> </md-button>
            </md-grid-tile>

            <md-grid-tile class="red">
                <md-button href="http://redoak-beta.azurewebsites.net" class="md-raised"> <h4>Beta Site</h4> </md-button>
            </md-grid-tile>

        </md-grid-list>
    </div>

</body>
</html>
</asp:Content>
