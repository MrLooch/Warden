(function () {
    'use strict';

    angular
        .module('wardenapp')         
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$location'];

    function DashboardController($location) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Dashboard';
    }
       
})();
