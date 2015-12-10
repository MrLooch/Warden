(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('SiteQueryController', SiteQueryController)
        .controller('SiteAddController', SiteAddController);

    //-----------------------------------------------
    // Query Controller
    //-----------------------------------------------
    SiteQueryController.$inject = ['Site'];

    function SiteQueryController(Site) {
        var vm = this;
        vm.sites = Site.query();
        vm.newSite = new Site();
        vm.newSite.Name = "Google";
        vm.newSite.Address = "Sydney Australia";
        vm.test = "Looch";

        vm.getData = function () {
            return "Hello Function";
        };

        vm.addSite = function () {            
            vm.newSite.$save();
            vm.sites.push(vm.newSite);
        };
    }

    //-----------------------------------------------
    // Create Controller
    //-----------------------------------------------
    SiteAddController.$inject = ['$scope', '$location', 'Site'];

    function SiteAddController($location, Site) {
        var vm = this;
        vm.site = new Site();
        vm.add = function () {
            vm.site.save(function () {
                $location.path('/');
            })
        }
    }
})();
