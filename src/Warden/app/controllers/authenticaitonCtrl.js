﻿(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('AuthenticaitonCtrl', AuthenticaitonCtrl);

    
    AuthenticaitonCtrl.$inject = ['authService','$location', 'ngDialog', '$log', '$scope'];
    
    function AuthenticaitonCtrl(authService,$location, ngDialog, $log, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'registerController';
        $log.debug("Just started register controller!");

        vm.username = null;
        vm.email = null;
        vm.password = null;
        vm.errorMessage = null;
        vm.isUserLoggedIn = false;

        function registerCommand(username, email, password) {
            // Set default GUID for ID to empty
            var id = "00000000-0000-0000-0000-000000000000";

            authService.register(id, username, email, password).then(function (status) {
                $log.debug("Signed up user " + vm.username + " status is " + status.status);
                //$routeParams.redirect will have the route
                //they were trying to go to initially                

                // Save the credentials

                // Add login use name to navigation bar
                vm.isUserLoggedIn = true;
                
                // Redirect the path to the user dashboard
                $location.path('/dashboard');

                ngDialog.closeAll();
            }, function (error) {
                $log.error("Registration failed " + error.status);
                vm.isUserLoggedIn = false;
            });
        }

        function loginCommand(email, password) {
            // Set default GUID for ID to empty
            var id = "00000000-0000-0000-0000-000000000000";

            authService.login(id,  email, password).then(function (status) {
                $log.debug("Logged in user " + email + " status is " + status.status);
                //$routeParams.redirect will have the route
                //they were trying to go to initially
                //if (status.status != 200) {
                //    vm.errorMessage = status.data.UserName[0];
                //    return;
                //}

                //if (status && $routeParams && $routeParams.redirect) {
                //    path = path + $routeParams.redirect;
                //}

                vm.isUserLoggedIn = true;

                $location.path('/dashboard');

                ngDialog.closeAll();
            });
        }

        function logoutUser() {
            authService.logout();
            $location.path('/');

        }

        // Send login registration details
        vm.signup = function () {
            registerCommand(vm.username, vm.email, vm.password);
        }

        // Send login registration details
        vm.login = function () {
            loginCommand(vm.email, vm.password);
        }
        vm.logout = function () {
            logoutUser();
        }

        vm.loginUser = function () {
            ngDialog.open({
                template: 'pages/login.html',
                plain: false,
                className: 'ngdialog-theme-default',
                scope: $scope,
                controller: 'AuthenticaitonCtrl',
                controllerAs: 'vm'
            });
        }

        // Set the create new site visiblity state
        vm.showsignup = function () {
            ngDialog.open({
                template: 'pages/signup.html',
                plain: false,
                className: 'ngdialog-theme-default',
                scope: $scope,
                controller: 'AuthenticaitonCtrl',
                controllerAs: 'vm'
            });
        }
        
    }
})();
