(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$location', 'ngDialog', '$scope'];

    function HomeController($location, ngDialog, $scope) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'homeController';

        activate();

        function activate() { }

        // Set the create new site visiblity state
        vm.showsignup = function () {
            ngDialog.open({
                template: 'pages/signup.html',
                plain: false,
                className: 'ngdialog-theme-default',
                scope: $scope
            });
        }
    }
})();
