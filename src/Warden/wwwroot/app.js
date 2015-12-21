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
    a.$inject = [ "$routeProvider", "$locationProvider", "$logProvider" ], angular.module("wardenapp", [ "ngRoute", "ngResource" ]).config(a);
}(), function() {
    "use strict";
    function a(a, b, c) {
        var d = this;
        d.newSite = {}, d.newSite.Id = 1, d.sites = {}, d.isEditVisible = !1, d.showEdit = function() {
            d.isEditVisible = !0;
        }, d.addNewSite = function(a) {
            d.isEditVisible = !1, d.insertSite(d.newSite), d.newSite = {};
        }, d.getSites = function() {
            b.message = "get all sites", a.getSites().then(function(a) {
                d.sites = a.data;
            }, function(a) {
                c.alert(a.message);
            });
        }, d.insertSite = function(c) {
            b.message = "insert new site " + c.Name, a.insertSite(c), d.sites.push(c);
        }, d.getSites();
    }
    angular.module("wardenapp").controller("SiteQueryController", a), a.$inject = [ "siteService", "$log", "$window" ];
}(), function() {
    "use strict";
    function a(a, b) {
        var c = "/api/sites/", d = {};
        return d.getSites = function() {
            return b.get(c);
        }, d.insertSite = function(a) {
            return b.post(c, a).then(function(a) {
                return a.data;
            });
        }, d;
    }
    angular.module("wardenapp").factory("siteService", a), a.$inject = [ "$resource", "$http" ];
}();