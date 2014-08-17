var GaappAngularApp = angular.module('GaappAngularApp', ['ngRoute', 'ui.bootstrap']);

GaappAngularApp.controller('LandingPageController', LandingPageController);
GaappAngularApp.controller('LoginController', LoginController);
GaappAngularApp.controller('RegisterController', RegisterController);

GaappAngularApp.factory('AuthHttpResponseInterceptor', AuthHttpResponseInterceptor);
GaappAngularApp.factory('LoginFactory', LoginFactory);
GaappAngularApp.factory('RegistrationFactory', RegistrationFactory);

var configFunction = function ($routeProvider, $httpProvider, $locationProvider) {

    $locationProvider.hashPrefix('!').html5Mode(true);

    $routeProvider.
        when('/routeOne', {
            templateUrl: 'routesDemo/one'
        })
        .when('/routeTwo/:donuts', {
            templateUrl: function (params) { return '/routesDemo/two?donuts=' + params.donuts; }
        })
        .when('/routeThree', {
            templateUrl: 'routesDemo/three'
        })
        .when('/login?returnUrl', {
            templateUrl: 'Account/Login',
            controller: LoginController
        })
        .when('/register', {
            templateUrl: '/Account/Register',
            controller: RegisterController
        });

    $httpProvider.interceptors.push('AuthHttpResponseInterceptor');
}
configFunction.$inject = ['$routeProvider', '$httpProvider', '$locationProvider'];

GaappAngularApp.config(configFunction);