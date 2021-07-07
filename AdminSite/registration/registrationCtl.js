angular.module('admin', ['ngMaterial'])

    .controller('registrationCtl', function ($scope, $http) {
        $scope.message = "";
        $scope.selected = true;
        $scope.success = false;
        $scope.settings = {};
        $scope.companyDict = {};
        $scope.message = "";
        $scope.template = "";
        $scope.device = null;
        $scope.companies = null;
        $scope.company = {
            Name: null,
            Id: null
        };
        $scope.getFile = function () {
            var reader = new FileReader();
            reader.onload = function (e) {
                var text = reader.result;
                $scope.toDictionary(text);
            };

            reader.readAsText($scope.file);	
            
        };

        $scope.toDictionary = (text) => {
            try {
                // split lines by '\n'
                var lines = text.split('\n');
                // split lines by '='
                lines.forEach(function (line) {
                    var setting = line.split('=');
                    if (setting.length > 1) {
                        // remove  '\r'
                        var set = setting[1].split('\r');
                        // populate settings dictionary
                        $scope.settings[setting[0]] = set[0];
                    }
                });
                // remove empty lines
                delete $scope.settings[""];
                // set text for continue button
                $scope.buttonText = "Register Device";
            }
            catch (error) {
                $scope.message = error;
            }
        };

        $scope.toCompanyDict = () => {
            $scope.companies.forEach(function (company) {
                $scope.companyDict[company.Id] = company;
            });
        };

        $scope.loadCompanies = () => {
            $http({
                url: "/api/companies",
                method: "GET"
            }).then(function successCallback(response) {
                // this callback will be called asynchronously
                // when the response is available
                $scope.companies = JSON.parse(response.data);
                // convert companies to dictionary
                $scope.toCompanyDict();
            }, function errorCallback(response) {
                // called asynchronously if an error occurs
                // or server returns response with an error status.
                $scope.message = response.statusText;
            });
        };

        $scope.addDevice = () => {

            var req = {
                method: 'POST',
                url: '/api/devices/new',
                data: "'" + JSON.stringify({ DeviceName: $scope.settings.machine, CompanyId: $scope.company.Id }) + "'"
            };

            $http(req).then(function successCallback(response) {
                $scope.message = "Device " + $scope.settings.machine + " successfully added to " + $scope.companyDict[$scope.company.Id].Name;
                $scope.success = true;
                $scope.writeToConfigFile();
            }, function errorCallback(response) {
                $scope.message = response.data;
            });
        };

        $scope.writeToConfigFile = () => {
            var config = "machine=" + $scope.settings['machine'] + "\r\n";
            config += "company=" + $scope.companyDict[$scope.company.Id].Name + "\r\n";

            //var blob = new Blob([config], { type: "string" });
            var blob = new Blob([config], { type: "text/plain;charset=utf-8" });

            saveAs(blob, "default.conf");
        };

        $scope.show = (template) => {
            $scope.template = template;
            if (template == 'device') {
                $scope.loadCompanies();
                $scope.selected = false;
            }
        };
    })

    .directive("ngFileSelect", function () {

        return {
            link: function ($scope, el) {

                el.bind("change", function (e) {

                    $scope.file = (e.srcElement || e.target).files[0];
                    $scope.getFile();
                });
            }
        };
    });