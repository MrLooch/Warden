(function () {
    'use strict';

    angular
        .module('wardenapp')         
        .controller('DashboardController', DashboardController);

    DashboardController.$inject = ['$scope', '$location', 'NgMap', '$log'];

    function DashboardController($scope, $location, NgMap, $log) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Dashboard';
        vm.types = "['address']";

        NgMap.getMap().then(function (map) {
            vm.map = map;
        });

        vm.placeChanged = function () {
            $log.debug("Placed changed");
            vm.place = this.getPlace();
            $log.debug('location ' +vm.place.geometry.location);
            vm.map.setCenter(vm.place.geometry.location);
        }
    }
})();
