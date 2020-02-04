angular.module("app")
    .service("AppService", [
        "$q", "$http", "LocalDataStorageService",
        function ($q, $http, localDataStorageService) {
            "use strict";

            var nextRoute = function() {
                var routeResponse = { ToState: "root.access-denied", IsLoggedIn: true, Broadcast: "loggedIn" };
                var user = localDataStorageService.getUserInfo();

                if (user === undefined) {
                    localDataStorageService.logout();
                    routeResponse = { ToState: "root.login", IsLoggedIn: false, Broadcast: "loggedOut" };
                } else if (user !== null) {
                    var flag = true;

                    for (var i = 0; i < user.RoleNames.length; i++) {

                        if (user.RoleNames[i] === "SystemAdmin") {
                            flag = false;
                            routeResponse.ToState = "root.dashboard";
                        } else if (user.RoleNames[i] === "Admin") {
                            flag = false;
                            routeResponse.ToState = "root.dashboard";
                        }
                        else if (user.RoleNames[i] === "Manager") {
                            flag = false;
                            routeResponse.ToState = "root.dashboard";
                        }
                    }

                    if (flag) routeResponse.ToState = "root.access-denied";
                }

                return routeResponse;
            };


            return {
                nextRoute: nextRoute
            };

        }
    ]);



angular.module("app")
    .service("UrlService", [
        function () {
            "use strict";

            var self = this;            

            self.url = "http://localhost:54652/";

            self.urls = [];
            self.urls.baseUrl = self.url;
            self.urls.baseApi = self.url + "api/";

            self.urls.TokenUrl = self.urls.baseUrl + "token";
            self.urls.AccountUrl = self.urls.baseApi + "Account";            
            self.urls.RoleUrl = self.urls.baseApi + "Role";            
            self.urls.ResourceUrl = self.urls.baseApi + "Resource";            
            self.urls.PermissionUrl = self.urls.baseApi + "Permission";
            self.urls.ProfileUrl = self.urls.baseApi + "Profile";
            self.urls.UserUrl = self.urls.baseApi + "User";
            self.urls.HomeUrl = self.urls.baseApi + "Home";
            self.urls.DashboardUrl = self.urls.baseApi + "Dashboard";
            self.urls.CustomerUrl = self.urls.baseApi + "Customer";
            self.urls.CaseUrl = self.urls.baseApi + "Case";
            self.urls.CaseLogUrl = self.urls.baseApi + "CaseLog";
            self.urls.PaymentUrl = self.urls.baseApi + "Payment";
            self.urls.ExpenseUrl = self.urls.baseApi + "Expense";
            self.urls.RevenueUrl = self.urls.baseApi + "Revenue";
            self.urls.ReportUrl = self.urls.baseApi + "Report";

            return self.urls;
        }
    ]);



angular.module("app")
    .service("AlertService", [
        "$rootScope", "$timeout", "$q", "$uibModal", "$log",
        function ($rootScope, $timeout, $q, $uibModal, $log) {
            "use strict";

            var alert = function (isAlert, type, msg, autoHide) {
                this.isAlert = isAlert;
                this.type = type;
                this.msg = msg;
                this.autoHide = autoHide;
                this.close = function () {
                    this.isAlert = false;
                };
            };

            this.showAlert = function (type, msg, closable) {
                $rootScope.alert = new alert(true, type, msg, !closable);
                if (!closable) {
                    $timeout(function () {
                        $rootScope.alert.close();
                    }, 2000);
                }
            };

            this.closeAlert = function () {
                $rootScope.alert.close();
            };

            this.alertType = {
                warning: "warning",
                success: "success",
                error: "error",
                info: "info",
                danger: "danger"
            };


            this.showConfirmDialog = function(size, data, action, configuration) {
                var self = this;
                self.deferred = $q.defer();

                if (!configuration)
                    configuration = { template: "app/views/modal/confirm.modal.tpl.html", controller: "ConfirmModalInstanceController" };
                else {
                    if (configuration.controller === "")
                        configuration.controller = "ConfirmModalInstanceController";
                }

                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: configuration.template,
                    controller: configuration.controller,
                    size: size,
                    resolve: {
                        action: function() {
                            return action;
                        },
                        data: function() {
                            return data;
                        }
                    }
                });

                modalInstance.result.then(function(modalResponse) {
                    self.deferred.resolve(modalResponse);
                }, function() {
                    $log.info("Modal dismissed at: " + new Date());
                    self.deferred.reject(false);
                });

                return self.deferred.promise;
            };

            this.actionType = {
                add: "add",
                save: "save",
                change: "change",
                edit: "edit",
                modify: "modify",
                update: "update",
                'delete': "delete",
                remove: "remove",
                load: "load",
                get: "get"
            };

            return {
                showAlert: this.showAlert,
                closeAlert: this.closeAlert,
                alertType: this.alertType,
                showConfirmDialog: this.showConfirmDialog,
                actionType: this.actionType
            };
        }
    ]);



angular.module("app")
    .service("LocalDataStorageService", [
        function () {
            "use strict";

            var app = "drscm_";

            var setToken = function(data) {
                var self = this;
                self.token = data.token_type + " " + data.access_token;
                localStorage.setItem(app + "token", self.token);
            };
            var getToken = function() {
                return (localStorage.getItem(app + "token") !== undefined) ? localStorage.getItem(app + "token") : undefined;
            };
            var deleteToken = function() {
                localStorage.removeItem(app + "token");
            };


            var setExpiresIn = function(date) {
                localStorage.setItem(app + "ExpiresIn", date);
            };
            var getExpiresIn = function() {
                return (localStorage.getItem(app + "ExpiresIn") !== undefined) ? localStorage.getItem(app + "ExpiresIn") : undefined;
            };
            var deleteExpiresIn = function() {
                localStorage.removeItem(app + "ExpiresIn");
            };


            var setUserInfo = function(user) {
                localStorage.setItem(app + "User", angular.toJson(user));
            };
            var getUserInfo = function() {
                return (localStorage.getItem(app + "User") !== undefined) ? angular.fromJson(localStorage.getItem(app + "User")) : undefined;
            };
            var deleteUserInfo = function() {
                localStorage.removeItem(app + "User");
            };


            var setUserRole = function(role) {
                localStorage.setItem(app + "UserRole", angular.toJson(role));
            };
            var getUserRole = function() {
                return (angular.fromJson(localStorage.getItem(app + "UserRole")) === undefined) ? undefined : angular.fromJson(localStorage.getItem("UserRole"));
            };
            var deleteUserRole = function() {
                localStorage.removeItem(app + "UserRole");
            };


            var logout = function() {
                deleteToken();
                deleteUserInfo();
                deleteExpiresIn();
                deleteUserRole();
            };


            return {
                setToken: setToken,
                getToken: getToken,
                deleteToken: deleteToken,

                setExpiresIn: setExpiresIn,
                getExpiresIn: getExpiresIn,
                deleteExpiresIn: deleteExpiresIn,

                setUserInfo: setUserInfo,
                getUserInfo: getUserInfo,
                deleteUserInfo: deleteUserInfo,

                setUserRole: setUserRole,
                getUserRole: getUserRole,
                deleteUserRole: deleteUserRole,

                logout: logout
            };
        }
    ]);



angular.module("app")
    .service("HttpService",[
        "$q", "$http",
        function ($q, $http) {
            "use strict";

            var get = function(url) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function(error) {
                    console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url).then(self.success, self.error);

                return self.deferred.promise;
            };

            var getByParams = function(url, data) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function(error) {
                    console.log(error);
                    return self.deferred.reject(error);
                };
                $http.get(url, { params: data }).then(self.success, self.error); //{params : data =~ {request : id}}

                return self.deferred.promise;
            };


            var add = function(url, data) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function(error) {
                    console.log(error);
                    return self.deferred.reject(error);
                };
                $http.post(url, JSON.stringify(data)).then(self.success, self.error);

                return self.deferred.promise;
            };


            var update = function(url, data) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function(error) {
                    console.log(error);
                    return self.deferred.reject(error);
                };
                $http.put(url, JSON.stringify(data)).then(self.success, self.error);

                return self.deferred.promise;
            };

            var remove = function(url, id) {
                var self = this;
                self.deferred = $q.defer();

                self.success = function(response) {
                    console.log(response);
                    if (response.status === 200) {
                        return self.deferred.resolve(response.data);
                    } else {
                        return self.deferred.reject(response);
                    }
                };
                self.error = function(error) {
                    console.log(error);
                    return self.deferred.reject(error);
                };
                $http.delete(url, { params: { request: id } }).then(self.success, self.error);

                return self.deferred.promise;
            };


            return {
                get: get,
                getByParams: getByParams,
                add: add,
                update: update,
                remove: remove
            };
        }
    ]);