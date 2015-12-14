!function() {
    "use strict";
    function a(a, b, c) {
        a.when("/", {
            templateUrl: "/pages/home.html"
        }).when("/about", {
            templateUrl: "/pages/about.html"
        }).when("/contact", {
            templateUrl: "/pages/contact.html"
        }).when("/sites", {
            templateUrl: "/pages/sites.html",
            controller: "SiteQueryController",
            controllerAs: "siteQueryController"
        }).otherwise({
            redirectTo: "/"
        }), b.html5Mode(!0), c.debugEnabled(!0);
    }
    a.$inject = [ "$routeProvider", "$locationProvider", "$logProvider" ], angular.module("wardenapp", [ "ngRoute" ]).config(a);
}(), function() {
    "use strict";
    function a(a, b, c) {
        function d() {
            b.message = "init message", e();
        }
        function e() {
            a.getSites().then(function(a) {
                f.sites = a.data;
            }, function(a) {
                c.alert(a.message);
            });
        }
        var f = this;
        d(), f.newSite = {}, f.newSite.Name = "Google", f.newSite.Address = "Sydney Australia", 
        f.test = "Looch", f.sites = {}, f.getData = function() {
            return "Hello Function";
        }, f.addSite = function() {}, f.insertSite = function() {
            f.sites.push(f.newSite), a.insertSite(f.newSite);
        };
    }
    function b(a, b) {
        var c = this;
        c.site = new b(), c.add = function() {
            c.site.save(function() {
                a.path("/");
            });
        };
    }
    angular.module("wardenapp").controller("SiteQueryController", a).controller("SiteAddController", b), 
    a.$inject = [ "siteService", "$log", "$window" ], b.$inject = [ "$scope", "$location", "Site" ];
}(), function() {
    "use strict";
    function a(a) {
        var b = "/api/sites/", c = {};
        return c.getSites = function() {
            return a.get(b);
        }, c.insertSite = function(c) {
            return a.post(b + "addSite", c).then(function(a) {
                return a.data;
            });
        }, c;
    }
    angular.module("wardenapp").factory("siteService", a), a.$inject = [ "$http" ];
}();