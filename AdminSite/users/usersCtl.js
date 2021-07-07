angular.module('ROI', ['ngMaterial'])
    .controller('usersCtl', function ($scope, $http, $window) {
        $scope.OutputMessage = '';
        $scope.Companies = null;
        $scope.Company = {
            Id: null,
            Name: null
        };
        $scope.SelectedCompany = {
            Id: null,
            Name: null
        };
        $scope.Users = null;
        $scope.User = {
            Id: null,
            Name: null,
            Password: null,
            Info: null,
            Active: true,
            CompanyId: null
        };
        $scope.Tests = null;
        $scope.template = 'userinfo';

        $scope.show = (template) => {
            if (template) {
                $scope.template = template;
            }
            if ($scope.template === 'edituser') {
                $scope.LoadCompanies();
            }
            else if ($scope.template === 'userinfo') {
                $scope.getChildren();
            }
            else if ($scope.template === 'newuser') {
                // clear the current user models
                $scope.resetUser();
                $scope.resetChildren();
                $scope.LoadCompanies();
            }
        };

        $scope.LoadCompanies = () => {
            $http({
                url: "/api/companies/getall",
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

        $scope.LoadUsers = () => {
            $http({
                url: "/api/users/getall",
                method: "GET"
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.Users = JSON.parse(response.data);
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.OutputMessage = response.statusText;
            });
        };

        $scope.getChildren = () => {
            // reset children
            //$scope.Devices = null;
            $scope.Tests = null;
            // query for companies
            // query for the company to which the user belongs
            $http({
                url: "/api/users/company/" + $scope.User.Id,
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Companies = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
            });
            // query for tests
            $http({
                url: "/api/tests/bytester/" + $scope.User.Id + "/" + $scope.utcOffset(),
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Tests = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
            });
        };

        $scope.addOrUpdateUser = () => {

            //$scope.User.CompanyId = $scope.SelectedCompany.Id;

            var req = {
                method: 'POST',
                url: '/api/users',
                data: $scope.User
            };

            $http(req).then(function successCallback(response) {
                $scope.LoadUsers();
                $scope.show('userinfo');
                $scope.User = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.deleteUser = () => {

            var req = {
                method: 'DELETE',
                url: '/api/users/' + $scope.User.UserName
            };

            $http(req).then(function successCallback(response) {
                $scope.resetUser();
                $scope.resetChildren();
                $scope.LoadUsers();
                $scope.show('userinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.resetUser = () => {
            $scope.User = {
                UserName: null,
                Password: null,
                CompanyName: null
            };
        };

        $scope.resetChildren = () => {
            $scope.Company = {
                Name: null,
                Company: null,
                Address: null,
                City: null,
                State: null,
                Zip: null,
                Contact: null,
                Phone: null,
                Email: null
            };
            $scope.Users = null;
            $scope.Tests = null;
            $scope.Devices = null;
        };

        $scope.utcOffset = () => {
            var date = new Date();
            return (-date.getTimezoneOffset()).toString();
        };

        $scope.openReport = (uuid, time) => {
            $window.open('https://reports.redoakinstruments.com/reports/Full.aspx?type=full&uuid=' + uuid + '&time=' + time + '&utcoffset=' + (-new Date().getTimezoneOffset()).toString());
        };

    }).config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('customTheme')
            .primaryPalette('grey')
            .accentPalette('orange')
            .warnPalette('red');
    });