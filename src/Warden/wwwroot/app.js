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
        }).when("/dashboard", {
            templateUrl: "/pages/dashboard.html",
            controller: "DashboardController",
            controllerAs: "dashboardController"
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
    angular.module("wardenapp", [ "ngRoute", "ngResource", "ui.grid", "ui.grid.edit", "ngDialog", "acute.select" ]).config(a);
}(), function() {
    "use strict";
    function a(a) {
        function b() {}
        var c = this;
        c.title = "Dashboard", c.data = {
            selectedShape: "Circle"
        }, c.shapes = [ "Square", "Circle", "Triangle", "Pentagon", "Hexagon" ], b();
    }
    angular.module("wardenapp").controller("DashboardController", a), a.$inject = [ "$location" ];
}(), function() {
    "use strict";
    function a(a, b, c, d, e) {
        function f(e, f, h) {
            var i = "00000000-0000-0000-0000-000000000000";
            a.login(i, e, f, h).then(function(a) {
                return d.debug("Signed up user " + g.username + " status is " + a.status), 200 != a.status ? void (g.errorMessage = a.data.UserName[0]) : (b.path("/dashboard"), 
                void c.closeAll());
            });
        }
        var g = this;
        g.title = "homeController", d.debug("Just started home controller!"), g.username = null, 
        g.email = null, g.password = null, g.errorMessage = null, g.loginUser = function() {
            d.debug("Login user");
        }, g.signup = function(a, b, c) {
            f(g.username, g.email, g.password);
        };
    }
    angular.module("wardenapp").controller("HomeController", a), a.$inject = [ "authService", "$location", "ngDialog", "$log", "$scope" ];
}(), function() {
    "use strict";
    function a(a, b, c, d, e) {
        function f(e, f, h) {
            var i = "00000000-0000-0000-0000-000000000000";
            a.login(i, e, f, h).then(function(a) {
                return d.debug("Signed up user " + g.username + " status is " + a.status), 200 != a.status ? void (g.errorMessage = a.data.UserName[0]) : (b.path("/dashboard"), 
                void c.closeAll());
            });
        }
        var g = this;
        g.title = "registerController", d.debug("Just started register controller!"), g.username = null, 
        g.email = null, g.password = null, g.errorMessage = null, g.signup = function(a, b, c) {
            f(g.username, g.email, g.password);
        }, g.showsignup = function() {
            c.open({
                template: "pages/signup.html",
                plain: !1,
                className: "ngdialog-theme-default",
                scope: e,
                controller: "RegisterCtrl",
                controllerAs: "vm"
            });
        };
    }
    angular.module("wardenapp").controller("RegisterCtrl", a), a.$inject = [ "authService", "$location", "ngDialog", "$log", "$scope" ];
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
    function a(a, b) {
        function c(a) {
            e.user.isAuthenticated = a;
        }
        var d = "/api/account/", e = {
            loginPath: "/register",
            user: {
                isAuthenticated: !1,
                roles: null
            }
        };
        return e.login = function(e, f, g, h) {
            return b.debug("Register user name " + f), a.post(d + "register", {
                Id: e,
                UserName: f,
                Email: g,
                Password: h
            }).then(function(a) {
                var d = a.status;
                return c(d), b.debug("Response status is " + a.status), a;
            }, function(a) {
                return b.debug("Failed sign up of user name " + f), a;
            });
        }, e.logout = function() {
            return a.post(d + "logout").then(function(a) {
                var b = !a.data.status;
                return c(b), b;
            });
        }, e;
    }
    angular.module("wardenapp").factory("authService", a), a.$inject = [ "$http", "$log" ];
}(), function() {
    "use strict";
    function a(a, b) {
        function c() {}
        var d = {
            getData: c
        };
        return d;
    }
    angular.module("wardenapp").service("sessionService", a), a.$inject = [ "$log", "localStorage" ];
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