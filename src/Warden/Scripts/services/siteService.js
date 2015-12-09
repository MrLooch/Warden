(function () {
    'use strict';
   
    angular
        .module('siteService', ['ngResource'])
        .factory('SiteQueryService', SiteQueryService);

    SiteQueryService.$inject = ['$resource'];

    function SiteQueryService($resource) {

        //this.query = function ()
       // {
            return $resource('/api/sites/');
        //}
    }

})();