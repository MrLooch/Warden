(function () {
    'use strict';

    angular
        .module('wardenapp')
        .constant('USER_ROLES', {
            all: '*',
            admin: 'admin',
            editor: 'editor',
            guest: 'guest'
        })
})();