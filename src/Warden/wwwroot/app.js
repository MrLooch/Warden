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
    angular.module("wardenapp", [ "ngRoute", "ngResource", "ui.grid", "ui.grid.edit", "ngDialog", "acute.select", "LocalStorageModule", "common.core" ]).config(a);
}(), function() {
    "use strict";
    angular.module("wardenapp").constant("AUTH_EVENTS", {
        loginSuccess: "auth-login-success",
        loginFailed: "auth-login-failed",
        logoutSuccess: "auth-logout-success",
        sessionTimeout: "auth-session-timeout",
        notAuthenticated: "auth-not-authenticated",
        notAuthorized: "auth-not-authorized"
    });
}(), function() {
    "use strict";
    angular.module("wardenapp").constant("USER_ROLES", {
        all: "*",
        admin: "admin",
        editor: "editor",
        guest: "guest"
    });
}(), function() {
    "use strict";
    function a(a, b, c, d, e) {
        function f(e, f, g) {
            var h = "00000000-0000-0000-0000-000000000000";
            a.register(h, e, f, g).then(function(a) {
                d.debug("Signed up user " + i.username + " status is " + a.status), i.isUserLoggedIn = !0, 
                b.path("/dashboard"), c.closeAll();
            }, function(a) {
                d.error("Registration failed " + a.status), i.isUserLoggedIn = !1;
            });
        }
        function g(e, f) {
            var g = "00000000-0000-0000-0000-000000000000";
            a.login(g, e, f).then(function(a) {
                return d.debug("Logged in user " + e + " status is " + a.status), 200 != a.status ? void (i.errorMessage = a.data.UserName[0]) : (i.isUserLoggedIn = !0, 
                b.path("/dashboard"), void c.closeAll());
            });
        }
        function h() {
            a.logout(), b.path("/");
        }
        var i = this;
        i.title = "registerController", d.debug("Just started register controller!"), i.username = null, 
        i.email = null, i.password = null, i.errorMessage = null, i.isUserLoggedIn = !1, 
        i.signup = function() {
            f(i.username, i.email, i.password);
        }, i.login = function() {
            g(i.email, i.password);
        }, i.logout = function() {
            h();
        }, i.loginUser = function() {
            c.open({
                template: "pages/login.html",
                plain: !1,
                className: "ngdialog-theme-default",
                scope: e,
                controller: "AuthenticaitonCtrl",
                controllerAs: "vm"
            });
        }, i.showsignup = function() {
            c.open({
                template: "pages/signup.html",
                plain: !1,
                className: "ngdialog-theme-default",
                scope: e,
                controller: "AuthenticaitonCtrl",
                controllerAs: "vm"
            });
        };
    }
    angular.module("wardenapp").controller("AuthenticaitonCtrl", a), a.$inject = [ "authService", "$location", "ngDialog", "$log", "$scope" ];
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
    angular.module("common.core", [ "ngRoute" ]);
}(), function(a) {
    "use strict";
    function b(a, b, c, d) {
        function e(d, e, f, g) {
            return a.get(d, e).then(function(a) {
                f(a);
            }, function(a) {
                "401" == a.status ? (c.displayError("Authentication required."), b.path("/login")) : null != g && g(a);
            });
        }
        function f(d, e, f, g) {
            return a.post(d, e).then(function(a) {
                f(a);
            }, function(a) {
                "401" == a.status ? (c.displayError("Authentication required."), b.path("/login")) : null != g && g(a);
            });
        }
        var g = {
            get: e,
            post: f
        };
        return g;
    }
    a.factory("apiService", b), b.$inject = [ "$http", "$location", "notificationService", "$rootScope" ];
}(angular.module("common.core")), function() {
    "use strict";
    function a(a, b, c, d, e) {
        function f(a) {
            b.debug("Registration failed");
        }
        var g = "/api/account/", h = {
            loginPath: "/register",
            authentication: {
                isAuthenticated: !1,
                email: "",
                roles: null
            }
        };
        return h.register = function(c, d, e, f) {
            return b.debug("Register user name " + e), a.post(g + "register", {
                Id: c,
                UserName: d,
                Email: e,
                Password: f
            }).then(function(a) {
                return h.storeUser(e, f), b.debug("Response status is " + a.status), a;
            }, function(a) {
                return b.debug("Failed sign up of user name " + e), h.logout(), a;
            });
        }, h.registerUser = function(a, c) {
            b.debug("Register user name " + a.Name), e.post(g + "register", a, c, f);
        }, h.login = function(c, d, e) {
            return b.debug("Login with email" + d), a.post(g + "login", {
                Id: c,
                Email: d,
                Password: e
            }).then(function(a) {
                return b.debug("Response status is " + a.status), h.storeUser(d, e), a;
            }, function(a) {
                return b.debug("Failed sign up of user name " + d), h.logout(), a;
            });
        }, h.logout = function() {
            h.clearCache();
        }, h.storeUser = function(a, b) {
            c.set("authorizationData", {
                Email: a
            }), h.authentication.isAuthenticated = !0, h.authentication.email = a, h.authentication.roles = d.all;
        }, h.clearCache = function() {
            c.remove("authorizationData"), h.authentication.isAuthenticated = !1, h.authentication.email = "", 
            h.authentication.roles = "";
        }, h;
    }
    angular.module("wardenapp").factory("authService", a), a.$inject = [ "$http", "$log", "localStorageService", "USER_ROLES", "apiService" ];
}(), function(a) {
    "use strict";
    function b() {
        function a(a) {
            toastr.success(a);
        }
        function b(a) {
            Array.isArray(a) ? a.forEach(function(a) {
                toastr.error(a);
            }) : toastr.error(a);
        }
        function c(a) {
            toastr.warning(a);
        }
        function d(a) {
            toastr.info(a);
        }
        toastr.options = {
            debug: !1,
            positionClass: "toast-top-right",
            onclick: null,
            fadeIn: 300,
            fadeOut: 1e3,
            timeOut: 3e3,
            extendedTimeOut: 1e3
        };
        var e = {
            displaySuccess: a,
            displayError: b,
            displayWarning: c,
            displayInfo: d
        };
        return e;
    }
    a.factory("notificationService", b);
}(angular.module("common.core")), function() {
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