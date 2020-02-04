angular.module("app")
    .controller("CaseDetailController", [
        "$scope", "UrlService", "HttpService", "AlertService", "$state", "LocalDataStorageService",
        function ($scope, urlService, httpService, alertService, $state, localDataStorageService) {
            "use strict";

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.model = { CustomerId: "", CaseNo: "", Brand: "", Model: "", Capacity: "", InterfaceType: "", Sl: "", RequiredData: "", Note: "", Status: "Received", ReceiveDate: new Date(), DeliveryDate: new Date(), DiscountPercent: 0, DiscountAmount: 0, TotalCost: 0, PaidAmount: 0, DueAmount: 0 };

                $scope.status = ["All", "Received", "Inspected", "Quoted", "Approved", "Processing", "Pending", "Delivered", "Canceled"];

                $scope.customer = [];
                $scope.caseLog = [];

                var state = $state;
                $scope.loadCase(state.params.id);

                $scope.NextStatus = "";

                $scope.updateUrl = urlService.CaseUrl;
                $scope.isDisabled = false;

                $scope.payment = { HddInfoId: "", CustomerId: "", PaymentDate: new Date().toLocaleString(), Amount: "" };
                $scope.payments = [];

                $scope.isUpdateMode = false;
            };

            $scope.loadCase = function(id) {
                httpService.getByParams(urlService.CaseUrl, {request: id}).then(function(data) {
                    console.log(data);
                    $scope.model = data;
                    $scope.NextStatus = $scope.getNextStatus();
                    $scope.loadCustomer($scope.model.CustomerId);
                    $scope.isDisabled = $scope.model.Status === "Delivered" || $scope.model.Status === "Canceled";
                }, function(error) {
                    console.log(error);
                });
            };


            $scope.loadCustomer = function(id) {
                httpService.getByParams(urlService.CustomerUrl, { request: id }).then(function(data) {
                    console.log(data);
                    $scope.customer = data;
                    $scope.loadCaseLogs($scope.model.Id);
                }, function(error) {
                    console.log(error);
                });
            };

            $scope.loadCaseLogs = function(id) {
                $scope.promise = httpService.getByParams(urlService.CaseLogUrl + "/GetCaseLog", { request: id }).then(function (data) {
                    console.log(data);
                    $scope.caseLog = data;
                    $scope.loadPayments($scope.model.Id);
                }, function(error) {
                    console.log(error);
                });
            };            

            $scope.save = function() {
                $scope.promise = httpService.add(urlService.CaseUrl, $scope.model).then(function (data) {
                    console.log(data);
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                    init();
                }, function(error) {
                    alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                    console.log(error);
                });
            };

            $scope.update = function () {
                $scope.promise = httpService.update($scope.updateUrl, $scope.model).then(function (data) {
                    console.log(data);
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                    init();
                }, function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                });
            };

            $scope.nextStatus = function (isCancel) {                
                if (!isCancel) {
                    for (var i = 0; i < $scope.status.length; i++) {
                        if ($scope.status[i] === $scope.model.Status) {
                            $scope.model.Status = $scope.status[i + 1];
                            break;
                        }
                    }
                } else {
                    $scope.model.Status = "Canceled";
                }
            };

            $scope.getNextStatus = function () {
                if ($scope.model.Status === "Delivered")
                    $scope.NextStatus = "Completed";
                else if ($scope.model.Status === "Canceled")
                    $scope.NextStatus = "Canceled";
                else {
                    for (var i = 0; i < $scope.status.length; i++) {
                        if ($scope.status[i] === $scope.model.Status) {
                            $scope.NextStatus = $scope.status[i + 1];
                            break;
                        }
                    }
                }
                                
                return $scope.NextStatus;
            };

            $scope.changeStatus = function (size, data, action, isCancel, isApprovedAgain) {
                data = { Id: data.Id, Name: "Status" };
                alertService.showConfirmDialog(size, data, action, false).then(function (response) {
                    console.log(response);
                    if (response.isConfirm) {
                        if (isApprovedAgain) {
                            $scope.model.Status = "Approved";
                        } else {
                            $scope.nextStatus(isCancel);
                        }
                        $scope.updateUrl = urlService.CaseUrl + "/UpdateStatus";
                        $scope.update();
                    }
                }, function (error) {
                    console.log(error);
                });
            };



            //payment section
            $scope.loadPayments = function (id) {
                $scope.promise = httpService.getByParams(urlService.PaymentUrl + "/GetListById", { request: id }).then(function (data) {
                    console.log(data);
                    $scope.payments = data;
                }, function (error) {
                    console.log(error);
                });
            };


            $scope.add = function () {
                if ($scope.isUpdateMode) {
                    $scope.modify();
                } else {
                    $scope.payment.HddInfoId = $scope.model.Id;
                    $scope.payment.CustomerId = $scope.model.CustomerId;

                    $scope.promise = httpService.add(urlService.PaymentUrl, $scope.payment).then(function (data) {
                        console.log(data);
                        alertService.showAlert(alertService.alertType.success, "Success", false);
                        init();
                    }, function (error) {
                        alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                        console.log(error);
                    });
                }
                
            };

            $scope.modify = function() {
                $scope.promise = httpService.update(urlService.PaymentUrl, $scope.payment).then(function (data) {
                    console.log(data);
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                    init();
                }, function (error) {
                    alertService.showAlert(alertService.alertType.danger, "Failed, Please try again", true);
                    console.log(error);
                });
            };

            $scope.loadPayment = function (id) {
                $scope.promise = httpService.getByParams(urlService.PaymentUrl, { request: id }).then(function (data) {
                    console.log(data);
                    $scope.payment = data;
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Payment load failed, Please try again, if not, check your internet connection", true);
                });
            };

            $scope.edit = function (id) {
                $scope.loadPayment(id);
                $scope.isUpdateMode = true;
            };


            $scope.delete = function (id) {
                $scope.promise = httpService.remove(urlService.PaymentUrl, id).then(function (data) {
                    console.log(data);
                    init();
                    alertService.showAlert(alertService.alertType.success, "Success", false);
                }, function (error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Payment delete failed, Please try again!", true);
                });
            };

            $scope.remove = function (size, data, action) {
                data = { Id: data.Id, Name: "Payment Amount " + data.Amount };
                alertService.showConfirmDialog(size, data, action, false).then(function (response) {
                    console.log(response);
                    if (response.isConfirm) {
                        $scope.delete(response.data.Id);
                    }
                }, function (error) {
                    console.log(error);
                });
            };


            $scope.print = function (size, data, action) {
                data = {
                    payment: data,
                    hddInfo: $scope.model,
                    customer: $scope.customer,
                    username: localDataStorageService.getUserInfo().FirstName + " " + localDataStorageService.getUserInfo().LastName
                };
                var configuration = {
                    template: "app/views/modal/payment-receipt.modal.tpl.html",
                    controller: "PaymentReceiptModalInstanceController"
                };
                alertService.showConfirmDialog(size, data, action, configuration).then(function (response) {
                    console.log(response);
                }, function (error) {
                    console.log(error);
                });
            };



            init();
        }
    ]);