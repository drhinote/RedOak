<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="devices.aspx.cs" Inherits="RedoakAdmin.devices" %>
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

    <!--Controller-->
    <script type="text/javascript" src="devicesCtl.js"></script>

    <!--underscore utilities library-->
    <script type="text/javascript" src="underscore-min.js"></script>
    <script type="text/javascript" src="underscore.js"></script>


</head>
<body ng-controller="devicesCtl" ng-cloak="" ng-app="ROI" ng-init="LoadDevices()">
    <br />
    <br />
    <br />
    <br />
    <div >
        <md-toolbar class="dimGrey">
            <div class="md-toolbar-tools dimGrey" style="background-color:dimgray">
                <h2 flex md-truncate><md-icon md-svg-icon="../img/gear.svg"></md-icon> Devices</h2>
                <md-input-container>
                    <label>Device</label>
                    <md-select placeholder="Select device" ng-model="Device" ng-model-options="{ trackBy: '$value.Name' }" ng-change="getChildren()" style="min-width: 200px;" >
                        <md-option ng-value="Device" ng-repeat="Device in Devices track by Device.Name">{{Device.Name}}</md-option>
                    </md-select>
                </md-input-container>
                <md-button ng-show="Device" class="btn1" ng-click="show('deviceinfo')">Info</md-button>
                <md-button class="btn1" ng-click="show('newdevice')">New</md-button>
                <md-button ng-show="Device" class="btn1" ng-click="show('editdevice')">Edit</md-button>
                <md-button ng-show="Device" class="btn1" ng-click="show('deletedevice')">Delete</md-button>               
            </div>
        </md-toolbar>

    </div>        
    <md-divider></md-divider>
    <br />
    <br />
    <br />
    <div ng-if="template" ng-include="'templates/' + template + '.html'" flex></div>
    <div ng-if="template == 'deletedevice'" ng-include="'templates/deviceinfo.html'" flex></div>
    

    <%--<br />
    <br />
    <br />
    <div style="margin: 10px; text-align: left; font-weight: 400;">
        <span style="font-family: 'Times New Roman', serif; color: rgb(140, 0, 0); font-style: italic; font-weight: 700; font-size: 32px; line-height: 1.16em;">Red Oak Instruments, LLC <img alt="" height="79" src="wp5082f3a7_06.png" width="81" /> </span>
    </div>--%>

</body>
</html>
</asp:Content>