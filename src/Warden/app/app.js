(function () {
    'use strict';

    config.$inject = ['$routeProvider', '$locationProvider', '$logProvider'];
    angular.module('wardenapp', ['ngRoute','ngResource']).config(config);

    function config($routeProvider, $locationProvider, $logProvider) {
        $routeProvider.when('/', {
                          templateUrl: '/pages/home.html'
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
                       .otherwise({
                           redirectTo: "/"
                       });


        $locationProvider.html5Mode(true);

        $logProvider.debugEnabled(true);
    }

})();