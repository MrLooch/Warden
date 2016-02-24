(function () {
    'use strict';

    angular
        .module('wardenapp')
        .service('sessionService', sessionService);

    sessionService.$inject = ['$log', 'localStorage'];

    function sessionService($log, localStorage) {
        var service = {
            getData: getData
        };

        return service;

        function getData() { }
    }
})();