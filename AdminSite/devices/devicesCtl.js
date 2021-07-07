var underscore = angular.module('underscore', []);
underscore.factory('_', function () {
    return window._; //Underscore should be loaded on the page
});

angular.module('ROI', ['ngMaterial', 'underscore'])
    .controller('devicesCtl', function ($scope, $http, _) {
        $scope.menuVisible = false;
        $scope.OutputMessage = '';
        $scope.Companies = null;
        $scope.Company = {
            Name: null,
            Id: null
        };
        $scope.Devices = null;
        $scope.Device = null;
        $scope.Tests = null;
        $scope.template = 'deviceinfo';
        

        $scope.showMenu = () => {
            //toggle
            if ($scope.menuVisible == true) {
                $scope.menuVisible = false;
            }
            else {
                $scope.menuVisible = true;
            }
        };

        $scope.updateCompany = (company) => {
            $scope.Device.CompanyId = company.Id;
        };

        $scope.show = (template) => {
            if (template) {
                $scope.template = template;
                $scope.menuVisible = false;
            }
            if ($scope.template == 'newdevice') {
                // clear the current comapny model
                $scope.resetDevice();
            }
        };

        $scope.LoadCompanies = () => {
            $http({
                url: "/api/companies/getall",
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Companies = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
            });
        };

        $scope.LoadDevices = () => {
            $http({
                url: "/api/devices",
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Devices = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
                });

            $scope.LoadCompanies();
        };

        $scope.getChildren = () => {
            // reset children
            $scope.Company = null;
            $scope.Tests = null;
            // query for company
            $http({
                url: "/api/devices/" + $scope.Device.Company_Id + "/company",
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Company = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
            });
            // query for test
            $http({
                url: "/api/devices/" + $scope.Device.Name + "/tests",
                method: "GET"
            }).then(function successCallback(response) {
                $scope.Tests = JSON.parse(response.data);
            }, function errorCallback(response) {
                $scope.OutputMessage = response.statusText;
            });
        };

        $scope.updateDevice = () => {
            var req = {
                method: 'POST',
                url: '/api/devices/update',
                data: "'" + JSON.stringify({ DeviceId: $scope.Device.Device_Id, CompanyId: $scope.Device.CompanyId }) + "'"
            };

            $http(req).then(function successCallback(response) {
                $scope.LoadDevices();
                $scope.show('deviceinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.newDevice = () => {

            $scope.Device.Id = null;
            $scope.Device.Serial = $scope.Device.Name;
            $scope.Device.CompanyId = $scope.Company.Id;
            $scope.Device.Enabled = true;

            var req = {
                method: 'POST',
                url: '/api/devices/new',
                data: $scope.Device
            };

            $http(req).then(function successCallback(response) {
                $scope.LoadDevices();

                $scope.Device = JSON.parse(response.data);
                $scope.getChildren();
                // select new device
                //$scope.Device = _.findWhere($scope.Devices, {
                //    'Device_Id': $scope.Device.Device_Id
                //});
                $scope.show('deviceinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.deleteDevice = () => {

            var req = {
                method: 'DELETE',
                url: '/api/devices/' + $scope.Device.Device_Id
            };

            $http(req).then(function successCallback(response) {
                $scope.resetDevice();
                $scope.LoadDevices();
                $scope.show('deviceinfo');
                $scope.OutputMessage = response.data;
            }, function errorCallback(response) {
                $scope.OutputMessage = response.data;
            });
        };

        $scope.resetDevice = () => {
            $scope.Device = {
                Name: null,
                Time: null,
                Company: null,
                Company_Name: null,
                Company_Id: null,
                Status: null,
                Device_Id: null
            };
        };

    }).config(function ($mdThemingProvider) {
        $mdThemingProvider.theme('customTheme')
            .primaryPalette('grey')
            .accentPalette('orange')
            .warnPalette('red');
    });