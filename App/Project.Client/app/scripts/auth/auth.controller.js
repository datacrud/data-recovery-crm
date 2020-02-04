﻿angular.module("app")
    .controller("LoginController", [
        "$rootScope", "$scope", "$state", "AuthService", "LocalDataStorageService", "AppService", "AlertService",
        function ($rootScope, $scope, $state, authService, localDataStorageService, appService, alertService) {
            "use strict";

            var init = function() {
                $scope.delay = 0;
                $scope.minDuration = 0;
                $scope.message = "Please Wait...";
                $scope.backdrop = true;
                $scope.promise = null;
            };
           
            $scope.credentials = { Username: "", Password: "", grant_type: "password" };

            $scope.login = function() {

                var changeRoute = function(isLoggedIn) {
                    if (isLoggedIn) {
                        var routeResponse = appService.nextRoute();

                        $scope.isLoggedIn = routeResponse.IsLoggedIn;
                        $rootScope.$broadcast(routeResponse.Broadcast);
                        $state.go(routeResponse.ToState);

                    } else {
                        $state.go("root.login");
                    }
                };


                var successCallback = function(response) {
                    console.log(response);

                    var success = function(response) {
                        console.log(response);
                        changeRoute(true);
                    };
                    var error = function(error) {
                        console.log(error);
                        changeRoute(false);
                    };
                    authService.userProfile().then(success, error);
                };
                var errorCallback = function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Login Faield! Incorrect Username or Password is detected!", true);
                    changeRoute(false);
                };
                $scope.promise = authService.authenticate($scope.credentials).then(successCallback, errorCallback);

            };


            init();
        }
    ]);



angular.module("app")
    .controller("AccessDeniedController", [
        "$scope", "$state", "$rootScope", "AppService",
        function ($scope, $state, $rootScope, appService) {

            $scope.defaultRoute = function() {
                var routeResponse = appService.nextRoute();

                $scope.isLoggedIn = routeResponse.IsLoggedIn;
                $rootScope.$broadcast(routeResponse.Broadcast);
                $state.go(routeResponse.ToState);
            };
        }
    ]);



angular.module("app")
    .controller("ProfileController", [
        "$scope", "UrlService", "LocalDataStorageService", "HttpService", "AlertService", "AuthService",
        function ($scope, urlService, localDataStorageService,  httpService, alertService, authService) {

            var init = function () {
                $scope.backdrop = true;
                $scope.promise = null;

                $scope.model = [];
                $scope.loadProfile();

                $scope.isDemoUser = authService.isDemoUser();                
            }; 

            $scope.loadProfile = function () {
                var success = function(data) {
                    $scope.model = data;
                };
                var error = function(error) {
                    console.log(error);
                };
                $scope.promise = httpService.get(urlService.ProfileUrl + "/UserProfile").then(success, error);
            };

            $scope.updateProfile = function() {
                var success = function(data) {
                    console.log(data);
                    if (data.Result.Succeeded) alertService.showAlert(alertService.alertType.success, "Success", false);
                    else alertService.showAlert(alertService.alertType.danger, "Failed! Please try agian", true);
                    $scope.loadProfile();
                };
                var error = function(error) {
                    console.log(error);
                    alert("User Profile Update Faield!");
                };
                $scope.promise = httpService.add(urlService.ProfileUrl + "/UpdateProfile", $scope.model).then(success, error);
            };

            $scope.updatePassword = function() {
                var requestModel = {
                    CurrentPassword: $scope.model.CurrentPassword,
                    NewPassword: $scope.model.NewPassword,
                    RetypePassword: $scope.model.RetypePassword
                };

                var success = function(data) {
                    console.log(data);
                    if (data) alertService.showAlert(alertService.alertType.success, "Success", false);
                    else alertService.showAlert(alertService.alertType.danger, "Failed! Please try agian", true);
                    $scope.loadProfile();
                };
                var error = function(error) {
                    console.log(error);
                    alertService.showAlert(alertService.alertType.danger, "Failed! Please try agian", true);
                };
                $scope.promise = httpService.add(urlService.ProfileUrl + "/UpdatePassword", requestModel).then(success, error);
            };


            init();
        }
    ]);