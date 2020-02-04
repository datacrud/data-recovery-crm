angular.module("app")
    .controller("DashboardController", [
        "$scope", "UrlService", "HttpService", "AlertService", "$state",
        function ($scope, urlService, httpService, alertService, $state) {
            "use strict";


            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.list = [];
                $scope.filters = [
                   "All", "Received", "Inspected", "Quoted", "Approved", "Processing", "Pending", "Delivered", "Canceled"
                ];
                $scope.searchRequest = { Keyword: "", Filter: "All" };

                $scope.loadCases();
            };            


            $scope.loadCases = function () {
                $scope.promise = httpService.get(urlService.CaseUrl).then(function (data) {
                    console.log(data);
                    $scope.list = data;
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Cases load failed, Please refresh the page, if not, check your internet connection", true);
                });
            };


            var columnDefs = [
                {
                    field: "CaseNo", displayName: "Case No",
                    cellTemplate: "<a ui-sref=\"root.case.detail({id: row.entity.Id})\" style=\"padding-left: 5px; padding-top: 5px\" ng-bind=\"row.getProperty(col.field)\"></a>"
                },
                { field: "Brand", displayName: "Brand" },
                { field: "Model", displayName: "Model" },
                { field: "Capacity", displayName: "Capacity" },
                {
                    field: "ReceiveDate", displayName: "Receive Date",
                    cellTemplate: "<div style=\"padding-left: 5px\" ng-bind=\"row.getProperty(col.field) | date : 'dd MMMM yyyy'\"></div>"
                },
                {
                    field: "DeliveryDate", displayName: "Delivery Date",
                    cellTemplate: "<div style=\"padding-left: 5px\" ng-bind=\"row.getProperty(col.field) | date : 'dd MMMM yyyy'\"></div>"
                },
                { field: "Status", displayName: "Status" },
                { field: "PaidAmount", displayName: "Paid" },
                { field: "DueAmount", displayName: "Due" }
            ];

            $scope.gridOptions = {
                data: "list",
                columnDefs: columnDefs,
                enablePinning: true,
                multiSelect: false,
                selectedItems: $scope.mySelections,
                enableCellSelection: false,
                enableRowSelection: true
            };


            $scope.search = function() {
                httpService.add(urlService.CaseUrl + "/Search", $scope.searchRequest).then(function (data) {
                    console.log(data);
                    $scope.list = data;
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.reset = function () {
                if ($scope.searchRequest.Keyword === undefined)
                    init();
            };


            init();
        }
    ]);