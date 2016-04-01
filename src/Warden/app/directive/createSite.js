(function() {
    'use strict';

    angular
        .module('wardenapp')
        .directive('createSite', createSite);

    function createSite () {
        // Usage:
        //     <createSite></createSite>
        // Creates:
        // 
        var directive = {
            restrict: 'E',
            replace: true,
            templateUrl: 'pages/components/createsite.html',
            scope: true
        };
        return directive;
    }

})();