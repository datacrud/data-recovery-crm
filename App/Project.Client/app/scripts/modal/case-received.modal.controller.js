angular.module("app")
    .controller("CaseReceivedModalInstanceController", [
        "$scope", "$uibModalInstance", "action", "data",
        function($scope, $uibModalInstance, action, data) {
            "use strict";

            $scope.today = new Date();
            $scope.action = action;
            $scope.data = data;

            $scope.response = {
                action: $scope.action,
                data: $scope.data,
                isConfirm: true
            };


            $scope.printDiv = function (divName) {
                var printContents = document.getElementById(divName).innerHTML;
                var originalContents = document.body.innerHTML;
                var popupWin;
                if (navigator.userAgent.toLowerCase().indexOf("chrome") > -1) {
                    popupWin = window.open("", "_blank", "width=750,height=600,scrollbars=no,menubar=no,toolbar=no,location=no,status=no,titlebar=no");
                    popupWin.window.focus();
                    popupWin.document.write("<!DOCTYPE html><html><head>" + "<link href=\"Content/bootstrap/bootstrap.min.css\" type=\"text/css\" rel=\"stylesheet\"/>" + "</head><body onload=\"window.print()\"><div style=\"width: 750px; height:auto;\">" + printContents + "</div></html>");
                    popupWin.onbeforeunload = function (event) {
                        popupWin.close();
                        return ".n";
                    };
                    popupWin.onabort = function(event) {
                        popupWin.document.close();
                        popupWin.close();
                    };
                } else {
                    popupWin = window.open("", "_blank", "width=750,height=auto", true);
                    popupWin.document.open();
                    popupWin.document.write("<html><head><link href=\"Content/bootstrap/bootstrap.min.css\" type=\"text/css\" rel=\"stylesheet\"/></head><body onload=\"window.print()\">" + printContents + "</html>");
                    popupWin.document.close();
                }
                popupWin.document.close();
                $scope.cancel();
                return true;
            };



            $scope.ok = function() {
                $uibModalInstance.close($scope.response);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);