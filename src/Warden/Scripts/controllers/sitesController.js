(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('SiteQueryController', SiteQueryController);

    SiteQueryController.$inject = ['SiteQueryService'];

    function SiteQueryController(SiteQueryService) {
        var vm = this;
        vm.sites = SiteQueryService.query();

        vm.test = "Looch";

        vm.getData = function () {
            return "Hello Function";
        };
    }
})();
