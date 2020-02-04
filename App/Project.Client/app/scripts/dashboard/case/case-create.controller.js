angular.module("app")
    .controller("CaseCreateController", [
        "$scope", "UrlService", "HttpService", "AlertService", "$state", "LocalDataStorageService",
        function ($scope, urlService, httpService, alertService, $state, localDataStorageService) {
            "use strict";

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.isUpdateMode = false;
                $scope.customers = [];
                $scope.customer = [];
                $scope.customerDropdownList = [];
                $scope.cases = [];
                $scope.searchRequest = { Keyword: "", Filter: "" };

                $scope.brands = [
                    "Maxtor", "Seagate", "Western Digital", "Samsung", "Toshiba", "Fujitsu", "HGST/Hitachi", "Transcend", "A-Data", "Apacer", "Others"
                ];

                $scope.interfaceTypes = [
                    "2.5\" SATA", "3.5\" SATA", "2.5\" IDE", "3.5\" IDE", "2.5\" USB", "3.5\" USB", "Others"
                ];

                $scope.model = { CustomerId: "", CaseNo: "", Brand: "", Model: "", Capacity: "", InterfaceType: "", Sl: "", RequiredData: "", Note: "", Status: "Received", ReceiveDate: new Date(), DeliveryDate: new Date(), TotalCost: 0, PaidAmount: 0, DueAmount: 0 };
                
                $scope.loadCases();
            };

            $scope.loadCustomerDropdownList = function() {
                httpService.get(urlService.CustomerUrl + "/CustomerDropdownList").then(function (data) {
                    console.log(data);
                    //$scope.customers = data;
                    $scope.customerDropdownList = data;

                    var state = $state;
                    if (state.params.id !== undefined) {
                        $scope.loadCase(state.params.id);
                        $scope.isUpdateMode = true;
                    }

                }, function(error) {
                    console.log(error);
                });
            };


            $scope.save = function () {
                if ($scope.isUpdateMode)
                    $scope.update();
                else {
                    $scope.promise = httpService.add(urlService.CaseUrl, $scope.model).then(function (data) {
                        console.log(data);
                        alertService.showAlert(alertService.alertType.success, "Success", false);
                        init();
                    }, function (error) {
                        alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                        console.log(error);
                    });
                }                
            };



            $scope.loadCase = function (id) {
                $scope.promise = httpService.getByParams(urlService.CaseUrl, { request: id }).then(function (data) {
                    console.log(data);
                    $scope.model = data;
                }, function (error) {
                    console.log(error);
                });
            };


            $scope.update = function() {
                $scope.model.Modified = new Date().toLocaleTimeString();
                $scope.promise = httpService.update(urlService.CaseUrl, $scope.model).then(function (data) {
                    console.log(data);
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                    init();
                }, function(error) {
                    alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                    console.log(error);
                });
            };


            //case received print
            $scope.loadCases = function() {
                $scope.promise = httpService.get(urlService.CaseUrl + "/GetPrintList").then(function (data) {
                    console.log(data);
                    $scope.cases = data;
                    $scope.loadCustomerDropdownList();
                }, function(error) {
                    console.log(error);
                });
            };


            $scope.print = function (size, data, action) {

                httpService.getByParams(urlService.CustomerUrl, { request: data.CustomerId }).then(function (response) {
                    console.log(response);
                    $scope.customer = response;

                    data = {
                        'case': data,
                        customer: $scope.customer,
                        username: localDataStorageService.getUserInfo().FirstName + " " + localDataStorageService.getUserInfo().LastName
                    };
                    var configuration = {
                        template: "app/views/modal/case-received.modal.tpl.html",
                        controller: "CaseReceivedModalInstanceController"
                    };
                    alertService.showConfirmDialog(size, data, action, configuration).then(function (response) {
                        console.log(response);
                    }, function (error) {
                        console.log(error);
                    });


                }, function (error) {
                    console.log(error);
                });

                
            };


            $scope.search = function() {
                $scope.promise = httpService.add(urlService.CaseUrl + "/SearchPrintableCase", $scope.searchRequest).then(function (data) {
                    console.log(data);
                    $scope.cases = data;
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.reset = function() {
                if ($scope.searchRequest.Keyword === undefined)
                    init();
            };



            init();
        }
    ]);