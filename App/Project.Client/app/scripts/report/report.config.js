angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            "use strict";

            $stateProvider
                .state("root.report", {
                    url: "/report",
                    views: {
                        "": {
                            templateUrl: "app/views/report/report.tpl.html",
                            controller: "ReportController"
                        }
                    }
                });
        }
    ]);