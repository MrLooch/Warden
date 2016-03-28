(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider', '$logProvider', 'ngDialogProvider'];
    angular.module('wardenapp', ['ngRoute', 'ngResource', 'ui.grid', 'ui.grid.edit', 'ngDialog', 'LocalStorageModule', 'common.core']).config(config);

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
                     .when('/dashboard', {
                         templateUrl: '/pages/dashboard.html',
                         controller: 'DashboardController',
                         controllerAs: 'dashboardController'
                     })                    
                     .otherwise({
                         redirectTo: "/"
                     });
        //GoogleMapApi.configure({
        //    //    key: 'your api key',
        //    v: '3.17',
        //    libraries: 'places'
        //});

        $locationProvider.html5Mode(true);

        $logProvider.debugEnabled(true);

        ngDialogProvider.setDefaults({
            className: "ngdialog-theme-default",
            plain: false,
            showClose: true,
            closeByDocument: true,
            closeByEscape: true,
            appendTo: false          
        });
    }
})();