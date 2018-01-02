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
    .factory('authenticationFactory', function ($http) {
      return {
        login: function(user) {
          return $http.post('login', user);
        }
      }
    })
    .controller('loginCtrl', function ($scope, authenticationFactory) {
      $scope.login = function () {
        authenticationFactory.login($scope.User).then(function (result) {
          console.log("logged!");
        }, function (error) {
          console.log(error);
        });
      };
    });
})(window, window.angular)