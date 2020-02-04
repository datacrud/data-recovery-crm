angular.module("app")
    .controller("ReportController", [
        "$scope", "UrlService", "HttpService", "AlertService",
        function ($scope, urlService, httpService, alertService) {
            "use strict";

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.model = { ReportFromDate: new Date(), ReportToDate: new Date(), ReportType: "" };
                $scope.reportTypes = ["Expense", "Revenue", "Discount", "Customer"];
                $scope.expenseReport = [];
                $scope.revenueReport = [];
                $scope.discountReport = [];
                $scope.customerReport = [];
                $scope.expenses = [];
                $scope.revenues = [];
                $scope.discounts = [];
                $scope.customers = [];
                $scope.view = "";
            };

            $scope.loadExpenseReport = function () {
                $scope.model.ReportFromDate = $scope.model.ReportFromDate.toDateString();
                $scope.model.ReportToDate = $scope.model.ReportToDate.toDateString();

                $scope.promise = httpService.add(urlService.ReportUrl, $scope.model ).then(function (data) {
                    console.log(data);
                    $scope.expenseReport = data;
                    $scope.expenses = $scope.expenseReport.Expenses;                    
                }, function(error) {
                    console.log(error);
                });

                $scope.model.ReportFromDate = new Date($scope.model.ReportFromDate);
                $scope.model.ReportToDate = new Date($scope.model.ReportToDate);
            };


            $scope.loadRevenueReport = function () {
                $scope.model.ReportFromDate = $scope.model.ReportFromDate.toDateString();
                $scope.model.ReportToDate = $scope.model.ReportToDate.toDateString();

                $scope.promise = httpService.add(urlService.ReportUrl, $scope.model).then(function (data) {
                    console.log(data);
                    $scope.revenueReport = data;
                    $scope.expenses = $scope.revenueReport.Expenses;
                    $scope.revenues = $scope.revenueReport.Revenues;                    
                }, function (error) {
                    console.log(error);
                });

                $scope.model.ReportFromDate = new Date($scope.model.ReportFromDate);
                $scope.model.ReportToDate = new Date($scope.model.ReportToDate);
            };

            $scope.loadDiscountReport = function () {
                $scope.model.ReportFromDate = $scope.model.ReportFromDate.toDateString();
                $scope.model.ReportToDate = $scope.model.ReportToDate.toDateString();

                $scope.promise = httpService.add(urlService.ReportUrl, $scope.model).then(function (data) {
                    console.log(data);
                    $scope.discountReport = data;
                    $scope.discounts = $scope.discountReport.Discounts;                    
                }, function (error) {
                    console.log(error);
                });

                $scope.model.ReportFromDate = new Date($scope.model.ReportFromDate);
                $scope.model.ReportToDate = new Date($scope.model.ReportToDate);
            };


            $scope.loadCustomerReport = function () {
                //$scope.model.ReportFromDate = $scope.model.ReportFromDate.toDateString();
                //$scope.model.ReportToDate = $scope.model.ReportToDate.toDateString();

                //$scope.promise = httpService.add(urlService.ReportUrl, $scope.model).then(function (data) {
                //    console.log(data);
                //    $scope.discountReport = data;
                //    $scope.discounts = $scope.discountReport.Discounts;                    
                //}, function (error) {
                //    console.log(error);
                //});

                $scope.model.ReportFromDate = new Date($scope.model.ReportFromDate);
                $scope.model.ReportToDate = new Date($scope.model.ReportToDate);
            };

            $scope.search = function () {
                $scope.view = "";
                $scope.expenseReport = [];
                $scope.revenueReport = [];
                $scope.discountReport = [];
                $scope.customerReport = [];
                $scope.expenses = [];
                $scope.revenues = [];
                $scope.discounts = [];
                $scope.customers = [];

                if ($scope.model.ReportType === $scope.reportTypes[0]) {
                    $scope.loadExpenseReport();
                    $scope.view = "expense";
                }

                if ($scope.model.ReportType === $scope.reportTypes[1]) {
                    $scope.loadRevenueReport();
                    $scope.view = "revenue";
                }

                if ($scope.model.ReportType === $scope.reportTypes[2]) {
                    $scope.loadDiscountReport();
                    $scope.view = "discount";
                }

                if ($scope.model.ReportType === $scope.reportTypes[3]) {
                    $scope.loadCustomerReport();
                    $scope.view = "customer";
                }
            };


            init();
        }
    ]);