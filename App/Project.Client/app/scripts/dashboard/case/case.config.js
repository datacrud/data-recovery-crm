angular.module("app")
    .config([
        "$stateProvider", "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            "use strict";

            $stateProvider
                .state("root.case", {
                    "abstract": true,
                    url: "/case",
                    views: {
                        "": {
                            template: "<div ui-view class=\"container-fluid\"></div>"
                        }
                    }
                })
                .state("root.case.create", {
                    url: "/create",
                    views: {
                        "": {
                            templateUrl: "app/views/dashboard/case/case-create.tpl.html",
                            controller: "CaseCreateController"
                        }
                    }
                })
                .state("root.case.edit", {
                    url: "/:id/edit",
                    views: {
                        "": {
                            templateUrl: "app/views/dashboard/case/case-create.tpl.html",
                            controller: "CaseCreateController"
                        }
                    }
                })
                .state("root.case.detail", {
                    url: "/:id",
                    views: {
                        "": {
                            templateUrl: "app/views/dashboard/case/case-detail.tpl.html",
                            controller: "CaseDetailController"
                        }
                    }
                });
        }
    ]);