angular.module("app")
    .controller("CustomerController", [
        "$scope", "UrlService", "HttpService", "AlertService",
        function ($scope, urlService, httpService, alertService) {
            "use strict";

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.model = {Code: "", Name: "",  Address: "", Phone: "", Email: "", CompanyName: "", Referance: ""};
                $scope.list = [];
                $scope.searchRequest = { Keyword: "" };
                $scope.loadCustomers();
                $scope.isUpdateMode = false;
            };

            $scope.loadCustomers = function () {
                $scope.promise = httpService.get(urlService.CustomerUrl).then(function (data) {
                    console.log(data);
                    $scope.list = data;
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Customers load failed, Please refresh the page or check your internet connection", true);
                });
            };

            $scope.save = function () {
                if ($scope.isUpdateMode) $scope.update();

                else {
                    $scope.promise = httpService.add(urlService.CustomerUrl, $scope.model).then(function (data) {
                        console.log(data);
                        alertService.showAlert(alertService.alertType.success, "Success", false);
                        init();
                    }, function (error) {
                        alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                        console.log(error);
                    });
                }
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

            $scope.edit = function (id) {
                $scope.loadCustomer(id);
                $scope.isUpdateMode = true;
            };


            $scope.update = function () {
                $scope.promise = httpService.update(urlService.CustomerUrl, $scope.model).then(function (data) {
                    console.log(data);
                    init();
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Customer update failed, Please try again!", true);
                });
            };

            $scope.delete = function (id) {
                $scope.promise = httpService.remove(urlService.CustomerUrl, id).then(function (data) {
                    console.log(data);
                    init();
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Customer delete failed, Please try again!", true);
                });
            };

            $scope.remove = function (size, data, action) {
                alertService.showConfirmDialog(size, data, action, false).then(function (response) {
                    console.log(response);
                    if (response.isConfirm) {
                        $scope.delete(response.data.Id);
                    }
                }, function (error) {
                    console.log(error);
                });
            };


            $scope.search = function () {
                $scope.promise = httpService.add(urlService.CustomerUrl + "/Search", $scope.searchRequest).then(function (data) {
                    console.log(data);
                    $scope.list = data;
                }, function (error) {
                    console.log(error);
                });
            };

            $scope.reset = function() {
                if ($scope.searchRequest.Keyword === undefined)
                    init();
            };

            $scope.cancel = function() {
                init();
            };


            init();
        }
    ]);