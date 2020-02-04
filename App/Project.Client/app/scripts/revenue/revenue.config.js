angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            "use strict";

            $stateProvider
                .state("root.revenue", {
                    url: "/revenue",
                    views: {
                        "": {
                            templateUrl: "app/views/revenue/revenue.tpl.html",
                            controller: "RevenueController"
                        }
                    }
                });
        }
    ]);