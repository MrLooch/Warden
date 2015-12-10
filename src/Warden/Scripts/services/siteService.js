(function () {
    'use strict';
   
    angular
       .module('siteServiceModule', ['ngResource'])
       .factory('Site', Site);

    Site.$inject = ['$resource'];

    function Site($resource) {

        var urlBase = '/api/sites/:id';
        return $resource(urlBase, { Id: "@Id" }, { "update": { method: "PUT" } });
    }

})();