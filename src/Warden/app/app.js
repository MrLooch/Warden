(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider', '$logProvider', 'ngDialogProvider'];
    angular.module('wardenapp', ['ngRoute', 'ngResource', 'ui.grid','ui.grid.edit', 'ngDialog']).config(config);

    function config($routeProvider, $locationProvider, $logProvider, ngDialogProvider) {
        $routeProvider.when('/', {
                        templateUrl: '/pages/home.html',
                        controller: 'HomeController',
                        controllerAs: 'homeCtrl'
                      })
                      .when('/about', {
                          templateUrl: '/pages/about.html'
                      })
                       .when('/contact', {
                           templateUrl: '/pages/contact.html'
                       })
                      .when('/sites', {
                          templateUrl: '/pages/sites.html',
                          controller: 'SiteQueryController',
                          controllerAs : 'siteQueryController'
                      })
                     .when('/signup', {
                            templateUrl: '/pages/signup.html',
                            controller: 'SiteQueryController',
                            controllerAs: 'siteQueryController'
                        })
                       .otherwise({
                           redirectTo: "/"
                       });


        $locationProvider.html5Mode(true);

        $logProvider.debugEnabled(true);

        //ngDialogProvider.setDefaults({
        //    className: "ngdialog-theme-default",
        //    plain: true,
        //    showClose: true,
        //    closeByDocument: true,
        //    closeByEscape: true,
        //    appendTo: false,
        //    preCloseCallback: function () {
        //        console.log("default pre-close callback");
        //    }
        //});
    }

})();