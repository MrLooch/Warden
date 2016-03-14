(function() {
    'use strict';

    angular
        .module('wardenapp')
        .directive('wardenNavbar', NavBarDirective);

    //NavBarDirective.$inject = [];
    
    function NavBarDirective() {
        // Usage:
        //     <directive></directive>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'A',
            templateUrl: 'pages/navbar.html',
            controller: 'AuthenticaitonCtrl', //Embed a custom controller in the directive,
            controllerAs: 'authCtrl',
            scope: {}
        };
        return directive;

        function link(scope, element, attrs) {            
        }
    }

})();