(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('SiteQueryController', SiteQueryController)
        .controller('SiteAddController', SiteAddController);

    //-----------------------------------------------
    // Query Controller
    //-----------------------------------------------
    SiteQueryController.$inject = ['siteService', '$log', '$window'];

    function SiteQueryController(siteService, $log, $window) {
        var vm = this;

        init();
        vm.newSite = {};
        vm.newSite.Name = "Google";
        vm.newSite.Address = "Sydney Australia";
        vm.test = "Looch";
        vm.sites = {};
        vm.getData = function () {
            return "Hello Function";
        };

        vm.addSite = function () {            
            //vm.newSite.$save();
            //vm.sites.push(vm.newSite);
        };

        function init() {

            $log.message = "init message";
            getSites();
        };

        function getSites() {
            siteService.getSites()
            .then(function (results) {
                vm.sites = results.data;
            }, function (error) {
                $window.alert(error.message);
            });
        };

        vm.insertSite = function () {
            vm.sites.push(vm.newSite);
            siteService.insertSite(vm.newSite);
        }
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
