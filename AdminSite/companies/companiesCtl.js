angular.module('ROI', ['ngMaterial'])
    .controller('companiesCtl', function ($scope, $http, $window) {
        $scope.OutputMessage = '';
        $scope.Companies = null;
        $scope.Company = {
            Company: null,
            Contact: null,
            Address: null,
            City: null,
            State: null,
            Zip: null,
            Phone: null,
            Email: null,
            IsActive: null,
        };
        $scope.Devices = null;
        $scope.Device = null;
        $scope.Users = null;
        $scope.Tests = null;
        $scope.template = 'companyinfo';

        $scope.show = (template) => {
            if (template) {
                $scope.template = template;
                $scope.menuVisible = false;
            }
            if ($scope.template == 'newcompany') {
                // clear the current comapny model
                $scope.resetChildren();
            }
        };

        $scope.refresh = () => {
            $scope.LoadCompanies();
            $scope.getChildren();
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

        $scope.getChildren = () => {
            // reset children
            $scope.Devices = null;
            $scope.Users = null;
            $scope.Tests = null;
            // query for devices
            $http({
                url: "/api/companies/" + $scope.Company.Id + "/devices",
                method: "GET"
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.Devices = JSON.parse(response.data);
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.OutputMessage = response.statusText;
            });
            // query for users
            $http({
                url: "/api/users/" + $scope.Company.Id,
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
            // query for tests
            $http({
                url: "/api/tests/" + $scope.Company.Id + "/" + $scope.utcOffset(),
                method: "GET"
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.Tests = JSON.parse(response.data);
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.OutputMessage = response.statusText;
            });
        };

        $scope.updateCompany = () => {

            var req = {
                method: 'POST',
                url: '/api/companies/update',
                data: "'" + JSON.stringify($scope.Company) + "'"
            };

            $http(req).then(function successCallback(response) {
                $scope.LoadCompanies();
                $scope.show('companyinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.newCompany = () => {

            var req = {
                method: 'POST',
                url: '/api/companies/new',
                data: "'" + JSON.stringify($scope.Company) + "'"
            };

            $http(req).then(function successCallback(response) {
                $scope.LoadCompanies();
                $scope.show('companyinfo');
                $scope.Company = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.deleteCompany = () => {

            var req = {
                method: 'DELETE',
                url: '/api/companies/' + $scope.Company.Id
            };

            $http(req).then(function successCallback(response) {
                $scope.resetChildren();
                $scope.LoadCompanies();
                $scope.show('companyinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
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

        $scope.openReport = (id, companyId) => {
            $window.open('https://reports.redoakinstruments.com/reports/Full.aspx?type=full&id=' + id + '&companyId=' + companyId + '&utcoffset=' + (-new Date().getTimezoneOffset()).toString());
        };

    }).config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('customTheme')
            .primaryPalette('grey')
            .accentPalette('orange')
            .warnPalette('red');
    });