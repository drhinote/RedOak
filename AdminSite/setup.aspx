<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="setup.aspx.cs" Inherits="RedoakAdmin.setup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" />


    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="Scripts/underscore-min.js"></script>

    <!-- Angular Material requires Angular.js Libraries -->
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-animate.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-aria.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular-messages.min.js"></script>

    <!-- Angular Material Library -->
    <script src="https://ajax.googleapis.com/ajax/libs/angular_material/1.1.8/angular-material.min.js"></script>
    <script type="text/javascript" src="../Scripts/Angular.js"></script>
</head>
<body ng-controller="adminCtl" ng-cloak="" ng-app="admin">
    <form runat="server">
        <%--field for communicating selected company with the codebehind--%>
        <asp:HiddenField runat="server" ID="hiddenSelectedCompany" Value="" />
    </form>
    <md-toolbar class="md-hue-2">
        <div class="md-toolbar-tools">
            <h2 class="md-flex">Register New Device: {{DeviceId}}</h2> <h2 ng-show="IsRegistered"> : {{Company.Name}}</h2>
        </div>
    </md-toolbar>

    <div ng-show="divFinish" class="md-body-1" style="padding-left:40px">
        <h2>All done!</h2> 
        <h3>Please click the <button>refresh</button> button in the device registration application to write the company information to your device.</h3>
    </div>
        

    <div ng-show="divCompany" >
        
        <div class="md-body-1" style="padding-left:40px">
            Would you like to register this device to a 
            <md-button class="md-raised" ng-click="ShowNewCompany()"> new </md-button> or 
            <md-button class="md-raised" ng-click="ShowExistingCompany()"> existing </md-button> company?
        </div>
    </div>

    <%--ADD NEW COMPANY--%>
    <div ng-show="divNewCompany">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add New Company</h2>
            </div>
        </md-toolbar>
        <div class="md-body-1"  style="padding-left:40px">
            Company Name: <input type="text" ng-model="NewCompany.Name" />
            Contact Name: <input type="text" ng-model="NewCompany.Contact" />
            Address: <input type="text" ng-model="NewCompany.Address" />
            City: <input type="text" ng-model="NewCompany.City" />
            State: <input type="text" ng-model="NewCompany.State" />
            Zip: <input type="text" ng-model="NewCompany.Zip" />
            Phone: <input type="text" ng-model="NewCompany.Phone" />
            Email: <input type="text" ng-model="NewCompany.Email" />
            <md-button class="md-raised" ng-click="AddDeviceToCompany()">Submit</md-button>
        </div>
    </div>

    <%--ADD EXISTING COMPANY--%>
    <div ng-show="divExistingCompany">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add {{DeviceId}} to Existing Company</h2>
            </div>
        </md-toolbar>
        <div class="md-body-1"  style="padding-left:40px">
            <md-input-container>
                <label>Company</label>
                <md-select placeholder="Assign to company" ng-model="Company" md-on-open="LoadCompanies()" style="min-width: 200px;">
                    <md-option ng-value="Company" ng-repeat="Company in Companies">{{Company.Name}}</md-option>
                </md-select>
            </md-input-container>
            <md-button class="md-raised" ng-click="AddDeviceToCompany()">Submit</md-button>
        </div>
    </div>

    <div ng-show="divUser">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add Users to {{Company.Name}}</h2>
            </div>
        </md-toolbar>
            
            <div class="md-body-1" style="padding-left:40px">Would you like to add a 
                <md-button class="md-raised" ng-click="ShowNewUser()">new</md-button> or
                <md-button class="md-raised" ng-click="ShowExistingUser()">existing</md-button> user to {{Company.Name}}?
                
            </div>
    </div>

    <%--ADD NEW USER--%>
    <div ng-show="divNewUser">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add New User to {{CompanyUser.Name}}</h2>
            </div>
        </md-toolbar>
            
            <div class="md-body-1" style="padding-left:40px">
                User Name: <input type="text" ng-model="CompanyUser.UserName" /> 
                {{CompanyUser.UserName}} Password: <input type="password" ng-model="CompanyUser.Password" />
                <md-button class="md-raised" ng-click="AddUserToCompany()">Submit</md-button>
            </div>
    </div>

    <%--ADD EXISTING USERS--%>

    <div ng-show="divExistingUser">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add User to Company</h2>
            </div>
        </md-toolbar>
        <div class="md-body-1"  style="padding-left:40px">
            <md-input-container>
                <label>Users</label>
                <md-select placeholder="Assign to company" ng-model="CompanyUser.UserName" md-on-open="LoadUsers()" style="min-width: 200px;">
                    <md-option ng-value="CompanyUser.UserName" ng-repeat="CompanyUser in AllUsers">{{CompanyUser.UserName}}</md-option>
                </md-select>
            </md-input-container>
            <md-button class="md-raised" ng-click="AddUserToCompany()">Submit</md-button>
        </div>
    </div>

    <%--<div ng-show="divExistingUser">
        <md-toolbar class="md-hue-1">
            <div class="md-toolbar-tools">
                <h2 class="md-flex">Add User to Existing Company</h2>
            </div>
        </md-toolbar>
        <div class="md-body-1"  style="padding-left:40px">
            <md-input-container>
                <label>Users</label>
                <md-select  ng-model="SelectedUsers"
                            md-on-open="LoadUsers()"
                            md-on-close="clearSearchTerm()"
                            data-md-container-class="selectdemoSelectHeader"
                            multiple>
                  <md-select-header class="demo-select-header">
                    <input ng-model="searchTerm"
                           type="search"
                           placeholder="Search for a user.."
                           class="demo-header-searchbox md-text">
                  </md-select-header>
                  <md-optgroup label="Users">
                    <md-option ng-value="User.Id" ng-repeat="User in AllUsers |
                      filter:searchTerm">{{User.Id}}</md-option>
                  </md-optgroup>
                </md-select>
              </md-input-container>
            <md-button class="md-raised" ng-click="AddUserToCompany()">Submit</md-button>
        </div>
    </div>--%>

    <label style="color: red; padding-left:40px">{{OutputMessage}}</label>

    <div ng-show="UpdateDevice" style="padding-left:40px">
        Would you like to <md-button class="md-raised" ng-click="GoToDevices()">update</md-button> this device instead?
    </div>

    <div ng-show="IsRegistered" style="padding-left:40px">
        <md-button  class="md-raised" ng-click="FinishSetup()">Finish Setup</md-button>
    </div>
    
    

    <script>
        angular.module('admin', ['ngMaterial']).controller('adminCtl', function ($scope, $http) {

            //$scope.searchTerm;
            //$scope.clearSearchTerm = function() {
            //    $scope.searchTerm = '';
            //};
            // The md-select directive eats keydown events for some quick select
            // logic. Since we have a search input here, we don't need that logic.
            //$element.find('input').on('keydown', function(ev) {
            //    ev.stopPropagation();
            //});

            $scope.Source = "";
            $scope.DeviceId = "";
            $scope.OutputMessage = "";
            $scope.NewUser = null;
            $scope.IsRegistered = false;
            $scope.divCompany = true;
            $scope.divNewCompany = false;
            $scope.divExistingCompany = false;
            $scope.divUser = false;
            $scope.divNewUser = false;
            $scope.divExistingUser = false;
            $scope.Company = {
                "Id": null,
                "Name": null
            };
            $scope.Companies = null;
            $scope.AllUsers = null;
            $scope.User = {
                "Id": null,
                "Password": null
            };
            $scope.NewDevice = {
                "DeviceId": null,
                "CompanyName": null
            };
            $scope.NewCompany = {
                "Name": null,
                "Contact": null,
                "Address": null,
                "City": null,
                "State": null,
                "Zip": null,
                "Phone": null,
                "Email": null
            };
            $scope.CompanyUser = {
                "CompanyName" : null,
                "UserName": null,
                "Password" : null
            };
            $scope.DeviceRegistrationStatus = {
                "Success": null,
                "Code": null,
                "Message": null
            };

            $scope.GetParams = () => {
                var params = window.location.search;
                $scope.Source = params.match("(desktop)")[1];
                if (!$scope.Source) $scope.Source = params.match("(mobile)")[1];
                $scope.DeviceId = params.match("id=(.+)$")[1];

            };
            $scope.ShowCompany = () => {
                $scope.HideAll();
                $scope.divCompany = true;
            };
            $scope.ShowNewCompany = () => {
                $scope.HideAll();
                $scope.divNewCompany = true;
            };
            $scope.ShowExistingCompany = () => {
                $scope.HideAll();
                $scope.divExistingCompany = true;
            };
            $scope.ShowUser = () => {
                $scope.HideAll();
                $scope.divUser = true;
            };
            $scope.ShowNewUser = () => {
                $scope.HideAll();
                $scope.divNewUser = true;
            };
            $scope.ShowExistingUser = () => {
                $scope.HideAll();
                $scope.divExistingUser = true;
            };
            $scope.AddCompany = (data) => {
                $http({
                    url: "/api/companies",
                    method: "POST",
                    data: JSON.stringify(data),
                }).then(function successCallback(response) {
                    $scope.IsRegistered = response.data;

                    return $scope.IsRegistered;
                    
                }, function errorCallback(response) {
                    $scope.OutputMessage = response.statusText;
                });

            };
            $scope.AddDeviceToCompany = () => {
                // existing company
                var data = null;
                // existing company
                if ($scope.Company.Id) {
                    $scope.IsRegistered = true;
                    // copy company stuff until the model can be verified (TODO)
                    $scope.CompanyUser.CompanyName = $scope.Company.Name;
                }
                // new company
                else {
                    var data = JSON.stringify($scope.NewCompany);
                    // copy company stuff until the model can be verified (TODO)
                    $scope.Company.Name = $scope.NewCompany.Name;
                    // copy company stuff until the model can be verified (TODO)
                    $scope.CompanyUser.CompanyName = $scope.NewCompany.Name;
                    $scope.IsRegistered = $scope.AddCompany(data);
                }

                $scope.NewDevice.DeviceId = $scope.DeviceId;
                $scope.NewDevice.CompanyName = $scope.Company.Name;
                //var data = JSON.stringify($scope.NewDevice);
                var data = JSON.stringify('{ "DeviceId": "' + $scope.NewDevice.DeviceId + '", "CompanyName": "' + $scope.NewDevice.CompanyName + '" }');
                $http({
                    url: "/api/devices",
                    method: "POST",
                    data: data,
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.DeviceRegistrationStatus = JSON.parse(response.data);

                    if ($scope.DeviceRegistrationStatus.Success) {
                        $scope.OutputMessage = $scope.DeviceRegistrationStatus.Message;
                        $scope.ShowUser();
                    }
                    else if ($scope.DeviceRegistrationStatus.Code == "DeviceRegistered") {
                        $scope.OutputMessage = $scope.DeviceRegistrationStatus.Message;
                        $scope.UpdateDevice = true;
                    }
                    
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    $scope.OutputMessage = response.statusText;
                });
                //}
            };
            $scope.AddUserToCompany = () => {
                $scope.CompanyUser.CompanyName = $scope.Company.Name;

                var test = JSON.stringify($scope.CompanyUser);
                var data = JSON.stringify('{ "CompanyName": "' + $scope.CompanyUser.CompanyName + '", "UserName": "' + $scope.CompanyUser.UserName + '", "Password": "' + $scope.CompanyUser.Password + '" }');

                // add user to copmany API call
                $http({
                    url: "/api/users",
                    method: "POST",
                    data: data,
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.OutputMessage = response.data;
                    $scope.ShowUser();
                    }, function errorCallback(response) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        $scope.OutputMessage = response.statusText;
                });
            };
            $scope.LoadUsers = () => {
                $http({
                    url: "/api/users",
                    method: "GET"
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.AllUsers = JSON.parse(response.data);
                }, function errorCallback(response) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                        $scope.OutputMessage = response.statusText;
                });
            };
            $scope.LoadCompanies = () => {
                $http({
                    url: "/api/companies",
                    method: "GET"
                }).then(function successCallback(response) {
                    // this callback will be called asynchronously
                    // when the response is available
                    $scope.Companies = JSON.parse(response.data);
                }, function errorCallback(response) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                    $scope.OutputMessage = response.statusText;
                });
            };
            $scope.GoToDevices = () => {
                window.location = "../devices/devices.aspx"
            };
            $scope.FinishSetup = () => {
                $scope.HideAll();
                $scope.divFinish = true;
                $scope.IsRegistered = false;
            };
            $scope.HideAll = () => {
                $scope.divCompany = false;
                $scope.divNewCompany = false;
                $scope.divExistingCompany = false;
                $scope.divUser = false;
                $scope.divNewUser = false;
                $scope.divExistingUser = false;
            };
            $scope.GetParams();
        });
    </script>
</body>
</html>
