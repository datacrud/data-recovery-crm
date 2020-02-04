angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            "use strict";

            $stateProvider
                .state("root.expense", {
                    url: "/expense",
                    views: {
                        "": {
                            templateUrl: "app/views/expense/expense.tpl.html",
                            controller: "ExpenseController"
                        }
                    }
                });
        }
    ]);