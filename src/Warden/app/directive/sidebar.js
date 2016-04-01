(function() {
    'use strict';

    angular
        .module('wardenapp')
        .directive('sideBar', sideBar);

    
    function sideBar() {
        // Usage:
        //     <sidebar></sidebar>
        // Creates:
        // 
        var directive = {            
            restrict: 'E',
            replace: true,
            templateUrl: 'pages/sideBar.html',
            scope: true
        };
        return directive;        
    }

})();