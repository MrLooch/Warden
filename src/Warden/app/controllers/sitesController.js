(function () {
    'use strict';

    angular
        .module('wardenapp')
        .controller('SiteQueryController', SiteQueryController);

    //-----------------------------------------------
    // Query Controller
    //-----------------------------------------------
    SiteQueryController.$inject = ['siteService', '$log', '$window'];

    function SiteQueryController(siteService, $log, $window) {
        var vm = this;

        // Store new site details
        vm.newSite = {};

        // Remove this at a later state
        vm.newSite.Id = 1;
        //vm.newSite.Name = "Google";
        //vm.newSite.Address = "Sydney Australia";

        vm.sites = {};

        vm.isEditVisible = false;

        vm.showEdit = function()
        {
            vm.isEditVisible = true;
        }
        
        // Add new site if it is valid
        vm.addNewSite = function(site)
        {
            vm.isEditVisible = false;
            vm.insertSite(vm.newSite);
            vm.newSite = {};
        }
        // Get all 
        vm.getSites = function () {
            $log.message = "get all sites";

            // Use the site service to get all the sites
            siteService.getSites()
            .then(function (results) {
                vm.sites = results.data;
            }, function (error) {
                $window.alert(error.message);
            });
        };

        // Insert the new site
        vm.insertSite = function (site) {
            $log.message = "insert new site " + site.Name;

            // Validate on clide side?            

            // Insert the new side on the server side.
            // Check if validation error occurs?
            siteService.insertSite(site);

            // Add the new site to the client side collection
            vm.sites.push(site);
        }

        // Get all the sites
        vm.getSites();
    }  
})();
