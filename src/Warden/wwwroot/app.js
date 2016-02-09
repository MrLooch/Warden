!function() {
    "use strict";
    function a(a, b, c, d) {
        a.when("/", {
            templateUrl: "/pages/home.html",
            controller: "HomeController",
            controllerAs: "homeCtrl"
        }).when("/about", {
            templateUrl: "/pages/about.html"
        }).when("/contact", {
            templateUrl: "/pages/contact.html"
        }).when("/sites", {
            templateUrl: "/pages/sites.html",
            controller: "SiteQueryController",
            controllerAs: "siteQueryController"
        }).when("/signup", {
            templateUrl: "/pages/signup.html",
            controller: "SiteQueryController",
            controllerAs: "siteQueryController"
        }).otherwise({
            redirectTo: "/"
        }), b.html5Mode(!0), c.debugEnabled(!0), d.setDefaults({
            className: "ngdialog-theme-default",
            plain: !1,
            showClose: !0,
            closeByDocument: !0,
            closeByEscape: !0,
            appendTo: !1
        });
    }
    a.$inject = [ "$routeProvider", "$locationProvider", "$logProvider", "ngDialogProvider" ], 
    angular.module("wardenapp", [ "ngRoute", "ngResource", "ui.grid", "ui.grid.edit", "ngDialog" ]).config(a);
}(), function() {
    "use strict";
    function a(a, b, c, d, e) {
        var f = this;
        f.title = "homeController", e.debug("Just started home controller!"), f.username = null, 
        f.email = null, f.password = null, f.errorMessage = null, f.login = function(a, b, c) {
            c.debug("Hello"), b.login(f.email, f.password).then(function(b) {
                return b ? (b && $routeParams && $routeParams.redirect && (path += $routeParams.redirect), 
                void a.path(path)) : void (f.errorMessage = "Unable to login");
            });
        }, f.showsignup = function() {
            b.open({
                template: "pages/signup.html",
                plain: !1,
                className: "ngdialog-theme-default",
                scope: c,
                controller: "HomeController",
                controllerAs: "vm"
            });
        };
    }
    angular.module("wardenapp").controller("HomeController", a), a.$inject = [ "$location", "ngDialog", "$scope", "authService", "$log" ];
}(), function() {
    "use strict";
    function a(a, b, c, d) {
        function e() {
            f.getSites();
        }
        var f = this;
        f.newSite = {}, f.newSite.Id = "00000000-0000-0000-0000-000000000000", f.sites = {}, 
        d.Delete = function(a) {
            b.debug("'Deleting row " + a), f.deleteSite(a);
        }, f.gridOptions = {
            columnDefs: [ {
                field: "Name",
                displayName: "Name"
            }, {
                field: "Address",
                displayName: "Address"
            }, {
                field: "href",
                name: "Action",
                cellEditableCondition: !1,
                cellTemplate: "pages/edit-button.html",
                enableFiltering: !1
            } ],
            multiSelect: !1,
            enableFiltering: !0,
            showColumnMenu: !1
        }, f.isEditVisible = !1, f.showEdit = function() {
            f.isEditVisible = !0;
        }, f.addNewSite = function(a) {
            f.isEditVisible = !1, f.insertSite(f.newSite), f.newSite = {};
        }, f.deleteSite = function(b) {
            a.deleteSite(b.entity.Id).then(function(a) {
                var c = f.gridOptions.data.indexOf(b.entity);
                f.gridOptions.data.splice(c, 1);
            }, function(a) {
                c.alert(a.message);
            });
        }, f.getSites = function() {
            b.message = "get all sites", a.getSites().then(function(a) {
                f.gridOptions.data = a.data, f.sites = a.data;
            }, function(a) {
                c.alert(a.message);
            });
        }, f.insertSite = function(c) {
            b.message = "insert new site " + c.Name, a.insertSite(c), f.sites.push(c);
        }, e();
    }
    angular.module("wardenapp").controller("SiteQueryController", a), a.$inject = [ "siteService", "$log", "$window", "$scope" ];
}(), function() {
    "use strict";
    function a(a) {
        function b(a) {
            d.user.isAuthenticated = a, $rootScope.$broadcast("loginStatusChanged", a);
        }
        var c = "/api/dataservice/", d = {
            loginPath: "/login",
            user: {
                isAuthenticated: !1,
                roles: null
            }
        };
        return d.login = function(d, e) {
            return a.post(c + "login", {
                userLogin: {
                    userName: d,
                    password: e
                }
            }).then(function(a) {
                var c = a.data.status;
                return b(c), c;
            });
        }, d.logout = function() {
            return a.post(c + "logout").then(function(a) {
                var c = !a.data.status;
                return b(c), c;
            });
        }, d.redirectToLogin = function() {
            $rootScope.$broadcast("redirectToLogin", null);
        }, d;
    }
    angular.module("wardenapp").factory("authService", a), a.$inject = [ "$http" ];
}(), function() {
    "use strict";
    function a(a, b) {
        var c = "/api/sites/", d = {};
        return d.getSites = function() {
            return b.get(c);
        }, d.insertSite = function(a) {
            return b.post(c, a).then(function(b) {
                return a.id = b.data.id, b.data;
            });
        }, d.updateSite = function(a) {
            return b.put(c, a).then(function(b) {
                return a.id = b.data.id, b.data;
            });
        }, d.deleteSite = function(a) {
            return b["delete"](c + a).then(function(a) {
                return a.data;
            });
        }, d;
    }
    angular.module("wardenapp").factory("siteService", a), a.$inject = [ "$resource", "$http" ];
}();