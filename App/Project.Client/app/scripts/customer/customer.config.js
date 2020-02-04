angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            "use strict";

            $stateProvider
                .state("root.customer", {
                    "abstract": true,
                    url: "/customer",
                    views: {
                        "": {
                            template: "<div ui-view class=\"container-fluid\"></div>"
                        }
                    }
                })
                .state("root.customer.list", {
                    url: "",
                    views: {
                        "": {
                            templateUrl: "app/views/customer/customer.tpl.html",
                            controller: "CustomerController"
                        }
                    }
                })
                .state("root.customer.detail", {
                    url: "/:id",
                    views: {
                        "": {
                            templateUrl: "app/views/customer/customer-detail.tpl.html",
                            controller: "CustomerDetailController"
                        }
                    }
                });
        }
    ]);