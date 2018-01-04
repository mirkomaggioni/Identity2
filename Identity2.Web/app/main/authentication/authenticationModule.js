(function (window, angular) {
  'use-strict';
  angular.module('authenticationModule', ['ui.router', 'toastr'])
    .config(function ($stateProvider) {
      $stateProvider.state('main.login', {
        url: '/main/login',
        templateUrl: 'app/main/authentication/login.html',
        controller: 'loginCtrl'
      });

      $stateProvider.state('main.register', {
        url: '/main/register',
        templateUrl: 'app/main/authentication/register.html',
        controller: 'registerCtrl'
      });
    })
    .factory('authenticationFactory', function ($http) {
      return {
        login: function(user) {
          return $http.post('login', user);
        },
        register: function (user) {
          return $http.post('register', user);
        }
      }
    })
    .controller('loginCtrl', function ($scope, toastr, authenticationFactory) {
      $scope.login = function () {
        authenticationFactory.login($scope.User).then(function (result) {
          toastr.success('logged!');
        }, function (error) {
          toastr.error(error);
        });
      };
    })
    .controller('registerCtrl', function ($scope, toastr, authenticationFactory) {
      $scope.register = function () {
        authenticationFactory.register($scope.User).then(function (result) {
          toastr.success('registered!');
        }, function (error) {
          toastr.error(error.data.Message);
        });
      };
    });
})(window, window.angular)