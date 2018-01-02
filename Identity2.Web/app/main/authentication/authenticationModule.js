(function (window, angular) {
  'use-strict';
  angular.module('authenticationModule', ['ui.router'])
    .config(function ($stateProvider) {
      $stateProvider.state('main.login', {
        url: '/main/login',
        templateUrl: 'app/main/authentication/login.html',
        controller: 'loginCtrl'
      });
    })
    .controller('loginCtrl', function () {

    });
})(window, window.angular)