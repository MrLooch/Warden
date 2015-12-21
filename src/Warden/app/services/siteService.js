(function () {
    'use strict';
   
    // Create a site service to perform CRUD operations
    angular
       .module('wardenapp')
       .factory('siteService', Site);

    Site.$inject = ['$resource','$http'];

    // Contains all the CRUD functions
    function Site($resource,$http) {

        var urlBase = '/api/sites/';
        var factory = {};

        // Get all the sites
        factory.getSites = function() {
            return $http.get(urlBase);

        };
        
        // Insert new site
        factory.insertSite = function (site) {
            return $http.post(urlBase, site)
                .then(function (results) {
                    return results.data;
                });
        };
        return factory;
    };

})();