(function () {
    'use strict';

    angular
        .module('wardenapp')
        .factory('authService', authFactory);

    authFactory.$inject = ['$http', '$log', 'localStorageService', 'USER_ROLES', 'apiService'];

    // Use web api to communicate with server login authenitcaion
    function authFactory($http, $log, localStorageService, USER_ROLES, apiService) {

        var serviceBase = '/api/account/';

        var factory = {
            loginPath: '/register',
            authentication: {
                isAuthenticated: false,
                email: "",
                roles: null
            }
        };

        // Registration
        factory.register = function (id, username, email, password) {
            $log.debug("Register user name " + email);
            return $http.post(serviceBase + 'register', { Id: id, UserName: username, Email: email, Password: password })
                .then(function (response) {                                        
                    factory.storeUser(email,password);
                    $log.debug("Response status is " + response.status);
                    return response;
                },
            function (responseHeaders) {
                $log.debug("Failed sign up of user name " + email);
                factory.logout();
                return responseHeaders;
            });
        };

       

        factory.registerUser = function (user, completed) {
            $log.debug("Register user name " + user.Name);
            apiService.post(serviceBase + 'register', user, completed, registrationFailed);
        };

        function registrationFailed(response) {
            $log.debug("Registration failed");
            //notificationService.displayError('Registration failed. Try again.');
        }

        // Log in user with credentials
        factory.login = function (id, email, password) {
            $log.debug("Login with email" + email);
            return $http.post(serviceBase + 'login', { Id: id, Email: email, Password: password })
                .then(function (response) {
                    $log.debug("Response status is " + response.status);
                    factory.storeUser(email,password);
                    return response;
                },
            function (responseHeaders) {
                $log.debug("Failed sign up of user name " + email);
                factory.logout();
                return responseHeaders;
            });
        };

        // Logout
        factory.logout = function () {
            factory.clearCache();                    
        };
        
        // Store login credentials into local storage
        factory.storeUser = function (email, password) {

            localStorageService.set('authorizationData', { Email: email });
            factory.authentication.isAuthenticated = true;
            factory.authentication.email = email;
            factory.authentication.roles = USER_ROLES.all;
        }

        // Remove login credentials from local storage
        factory.clearCache = function () {

            localStorageService.remove('authorizationData');

            factory.authentication.isAuthenticated = false;
            factory.authentication.email = "";
            factory.authentication.roles = "";

        };
         

        return factory;
    };
})();