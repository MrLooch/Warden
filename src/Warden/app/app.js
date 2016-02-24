(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider', '$logProvider', 'ngDialogProvider'];
    angular.module('wardenapp', ['ngRoute', 'ngResource', 'ui.grid', 'ui.grid.edit', 'ngDialog', 'acute.select']).config(config);

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
                     //.when('/signup', {
                     //       templateUrl: '/pages/signup.html',
                     //       controller: 'RegisterController',
                     //       controllerAs: 'registerCtrl'
                     //   })
                       .otherwise({
                           redirectTo: "/"
                       });


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