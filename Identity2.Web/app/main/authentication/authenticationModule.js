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
      var _isLogged = false;
      return {
        isLogged: function (logged) {
          _isLogged = logged;
        },
        isLogged: function () {
          return _isLogged;
        },
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
          authenticationFactory.isLogged(true);
          toastr.success('logged!');
        }, function (error) {
          authenticationFactory.isLogged(false);
          toastr.error(error);
        });
      };
    })
    .controller('registerCtrl', function ($scope, $state, toastr, authenticationFactory) {
      $scope.register = function () {
        authenticationFactory.register($scope.User).then(function (result) {
          toastr.success('registered!');
          $state.reload();
        }, function (error) {
          toastr.error(error.data.Message);
        });
      };
    });
})(window, window.angular)