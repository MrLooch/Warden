(function () {
    'use strict';

    angular
        .module('wardenapp')
        .factory('authService', authFactory);

    authFactory.$inject = ['$http', '$log'];

    // Use web api to communicate with server login authenitcaion
    function authFactory ($http, $log) {
        var serviceBase = '/api/account/';
        var factory = {
            loginPath: '/register',
            user: {
                isAuthenticated: false,
                roles: null
            }
        };

        factory.login = function (id,username, email, password) {
            $log.debug("Register user name " + username);
            return $http.post(serviceBase + 'register', { Id: id, UserName: username, Email: email, Password: password })
                .then(function (response) {
                    var loggedIn = response.status;
                    changeAuth(loggedIn);
                    $log.debug("Response status is " + response.status);
                    return response;
                },
            function (responseHeaders) {
                $log.debug("Failed sign up of user name " + username);
                return responseHeaders;
            });
        };

        factory.logout = function () {
            return $http.post(serviceBase + 'logout').then(
                function (results) {
                    var loggedIn = !results.data.status;
                    changeAuth(loggedIn);
                    return loggedIn;
        });
        };

            ////factory.redirectToLogin = function () {
            ////    $rootScope.$broadcast('redirectToLogin', null);
            ////};

            function changeAuth(loggedIn) {
                factory.user.isAuthenticated = loggedIn;
            //$rootScope.$broadcast('loginStatusChanged', loggedIn);
        }

        return factory;
    };
})();