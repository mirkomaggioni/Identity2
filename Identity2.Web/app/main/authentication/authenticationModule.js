(function (window, angular) {
  'use-strict';
  angular.module('authenticationModule', ['ui.router', 'toastr'])
    .config(function ($stateProvider) {
      $stateProvider.state('main.login', {
        url: '/login',
        templateUrl: 'app/main/authentication/login.html',
        controller: 'loginCtrl'
      });

      $stateProvider.state('main.register', {
        url: '/register',
        templateUrl: 'app/main/authentication/register.html',
        controller: 'registerCtrl'
      });
    })
    .factory('authenticationFactory', function ($http, $q) {
      var logged = false;
      var token = '';

      return {
        set isLogged(value) {
          logged = value;
        },
        get isLogged() {
          return logged;
        },
        login: function (user) {
          var deferred = $q.defer();
          var self = this;

          $http({
            method: 'POST',
            url: '/token',
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: {
              grant_type: 'password',
              username: user.Username,
              password: user.Password
            },
            transformRequest: function(obj) {
              var str = [];
              for (var p in obj) {
                if (obj.hasOwnProperty(p)) {
                  str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                }
              }
              return str.join("&");
            }
          }).then(function(result) {
            self.isLogged = true;
            token = result.data;
            deferred.resolve();
          }).catch(function() {
            self.isLogged = false;
            deferred.reject(error);
          });

          return deferred.promise;
        },
        register: function (user) {
          return $http.post('register', user);
        }
      }
    })
    .controller('loginCtrl', function ($scope, $state, toastr, authenticationFactory) {
      $scope.login = function () {
        authenticationFactory.login($scope.User).then(function () {
          toastr.success('logged!');
          $state.go('main.home');
        }, function (error) {
          toastr.error(error);
        });
      };
    })
    .controller('registerCtrl', function ($scope, $state, toastr, authenticationFactory) {
      $scope.register = function () {
        authenticationFactory.register($scope.User).then(function () {
          toastr.success('registered!');
          $state.reload();
        }, function (error) {
          toastr.error(error.data.Message);
        });
      };
    });
})(window, window.angular)