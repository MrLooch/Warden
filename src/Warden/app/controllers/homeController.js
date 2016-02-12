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
        vm.signUpDialog = null;
        function loginWithService(username, email, password) {
            var id = "00000000-0000-0000-0000-000000000000";
            authService.login(id,username, email, password).then(function (status) {
                $log.debug("Closing dialog 1 "+ status + vm.signUpDialog);
                //$routeParams.redirect will have the route
                //they were trying to go to initially
                //if (!status) {
                //    vm.errorMessage = 'Unable to login';
                //    return;
                //}

                //if (status && $routeParams && $routeParams.redirect) {
                //    path = path + $routeParams.redirect;
                //}

                //$location.path(path);
                
                ngDialog.closeAll();
            });
        }

        // Send login registration details
        vm.login = function ($location, authService, $log) {

            loginWithService(vm.username, vm.email, vm.password);
            
        }

        // Set the create new site visiblity state
        vm.showsignup = function () {
            vm.signUpDialog =  ngDialog.open({
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
