(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['authService','$location', 'ngDialog', '$log', '$scope'];
    
    function HomeController(authService,$location, ngDialog, $log, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'homeController';
        $log.debug("Just started home controller!");
        vm.username = null;
        vm.email = null;
        vm.password = null;
        vm.errorMessage = null;

        function loginWithService(username, email, password) {
            // Set default GUID for ID to empty
            var id = "00000000-0000-0000-0000-000000000000";

            authService.login(id,username, email, password).then(function (status) {
                $log.debug("Signed up user " + vm.username + " status is " + status.status);
                //$routeParams.redirect will have the route
                //they were trying to go to initially
                if (status.status != 200) {
                    vm.errorMessage = status.data.UserName[0];
                    return;
                }

                //if (status && $routeParams && $routeParams.redirect) {
                //    path = path + $routeParams.redirect;
                //}

                $location.path('/dashboard');
                
                ngDialog.closeAll();
            });
        }

        vm.loginUser = function () {
            $log.debug("Login user");
            // TODO: Open login dialog.
        }

        // Send login registration details
        vm.signup = function ($location, authService, $log) {
            loginWithService(vm.username, vm.email, vm.password);            
        }
    }
})();
