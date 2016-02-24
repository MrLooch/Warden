(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('RegisterCtrl', RegisterCtrl);

    
    RegisterCtrl.$inject = ['authService','$location', 'ngDialog', '$log', '$scope'];
    
    function RegisterCtrl(authService,$location, ngDialog, $log, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registerController';
        $log.debug("Just started register controller!");
        vm.username = null;
        vm.email = null;
        vm.password = null;
        vm.errorMessage = null;

        function loginWithService(username, email, password) {
            // Set default GUID for ID to empty
            var id = "00000000-0000-0000-0000-000000000000";

            authService.login(id, username, email, password).then(function (status) {
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

        // Send login registration details
        vm.signup = function ($location, authService, $log) {
            loginWithService(vm.username, vm.email, vm.password);
        }

        // Set the create new site visiblity state
        vm.showsignup = function () {
            ngDialog.open({
                template: 'pages/signup.html',
                plain: false,
                className: 'ngdialog-theme-default',
                scope: $scope,
                controller: 'RegisterCtrl',
                controllerAs: 'vm'
            });
        }
        
    }
})();
