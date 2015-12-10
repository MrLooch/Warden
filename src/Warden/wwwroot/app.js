!function() {
    "use strict";
    function a(a, b) {
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
        }), b.html5Mode(!0);
    }
    a.$inject = [ "$routeProvider", "$locationProvider" ], angular.module("wardenapp", [ "ngRoute", "siteServiceModule" ]).config(a);
}(), function() {
    "use strict";
    function a(a) {
        var b = this;
        b.sites = a.query(), b.newSite = new a(), b.newSite.Name = "Google", b.newSite.Address = "Sydney Australia", 
        b.test = "Looch", b.getData = function() {
            return "Hello Function";
        }, b.addSite = function() {
            b.newSite.$save(), b.sites.push(b.newSite);
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
    a.$inject = [ "Site" ], b.$inject = [ "$scope", "$location", "Site" ];
}(), function() {
    "use strict";
    function a(a) {
        var b = "/api/sites/:id";
        return a(b, {
            Id: "@Id"
        }, {
            update: {
                method: "PUT"
            }
        });
    }
    angular.module("siteServiceModule", [ "ngResource" ]).factory("Site", a), a.$inject = [ "$resource" ];
}();