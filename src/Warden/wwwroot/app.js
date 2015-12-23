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
    a.$inject = [ "$routeProvider", "$locationProvider", "$logProvider" ], angular.module("wardenapp", [ "ngRoute", "ngResource", "ui.grid", "ui.grid.edit" ]).config(a);
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
                field: "Id",
                name: "",
                cellTemplate: "pages/edit-button.html",
                width: 34,
                enableFiltering: !1
            }, {
                field: "Name",
                displayName: "Name"
            }, {
                field: "Address",
                displayName: "Address"
            }, {
                name: "Action",
                cellEditableCondition: !1,
                cellTemplate: '<button class ="btn btn-danger" ng-click="grid.appScope.Delete(row)">Delete</button>',
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