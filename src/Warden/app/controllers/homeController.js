(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$location', 'ngDialog', '$scope', 'authService', '$log'];

    function HomeController($location, ngDialog, $scope, authService, $log) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'homeController';
        $log.debug("Just started home controller!");
        vm.username = null;
        vm.email = null;
        vm.password = null;
        vm.errorMessage = null;
        
        vm.login = function ($location, authService, $log) {

            $log.debug("Hello");
            authService.login(vm.email, vm.password).then(function (status) {
                //$routeParams.redirect will have the route
                //they were trying to go to initially
                if (!status) {
                    vm.errorMessage = 'Unable to login';
                    return;
                }

                if (status && $routeParams && $routeParams.redirect) {
                    path = path + $routeParams.redirect;
                }

                $location.path(path);
            });        
        }

        // Set the create new site visiblity state
        vm.showsignup = function () {
            ngDialog.open({
                template: 'pages/signup.html',
                plain: false,
                className: 'ngdialog-theme-default',
                scope: $scope,
                controller: 'HomeController',
                controllerAs : 'vm'
            });
        }
    }
})();
