<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="RedoakAdmin.registration.registration" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
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

    <!--Controller definition for registration-->
    <script type="text/javascript" src="registrationCtl.js"></script>
    <script src="FileSaver.js"></script>
</head>
<body ng-controller="registrationCtl" ng-cloak="" ng-app="admin">
    <br />
    <br />
    <br />
    <br />
    <div >
        <image ng-show="selected" src="img/config.jpg"></image>
        <form>
            <input type="file" ng-file-select="onFileSelect($files)" >
            <md-button ng-show="selected" class="md-raised" ng-click="show('device')">Continue</md-button>
        </form>
    </div>
    <div ng-if="template" ng-include="'templates/' + template + '.html'" flex></div>
    {{message}}
    <br />
    <image ng-show="success" src="img/saveAs.jpg"></image>
    <image ng-show="success" src="img/replace.jpg"></image>
    
</body>
</html>
</asp:Content>