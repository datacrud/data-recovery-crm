angular.module("app")
    .controller("CustomerDetailController", [
        "$scope", "UrlService", "HttpService", "AlertService", "$stateParams",
        function ($scope, urlService, httpService, alertService, $stateParams) {
            "use strict";

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.model = {Code: "", Name: "",  Address: "", Phone: "", Email: "", CompanyName: "", Referance: ""};
                $scope.loadCustomer($stateParams.id);
            };

            $scope.loadCustomer = function (id) {
                $scope.promise = httpService.getByParams(urlService.CustomerUrl, { request: id }).then(function (data) {
                    console.log(data);
                    $scope.model = data;
                }, function (error) {
                    console.log(error);
                    init();
                    alertService.showAlert(alertService.alertType.danger, "Customer load failed, Please try again or check your internet connection", true);
                });
            };


            $scope.cancel = function() {
                init();
            };


            init();
        }
    ]);