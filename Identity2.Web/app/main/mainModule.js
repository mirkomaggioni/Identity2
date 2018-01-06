(function (window, angular) {
  'use-strict';
  angular.module('mainModule', ['ui.router', 'authenticationModule', 'toastr'])
    .config(function($stateProvider) {
      var mainState = {
        name: 'main',
        url: '/main',
        views: {
          'header': { templateUrl: 'app/main/header.html' },
          'main': { templateUrl: 'app/main/main.html' }
        }
      }

      $stateProvider.state(mainState);

      $stateProvider.state('main.home',
        {
          url: '/home',
          templateUrl: 'app/main/home.html',
          controller: 'homeCtrl'
        });
    })
    .controller('homeCtrl', function (toastr, authenticationFactory) {
      toastr.success(authenticationFactory.isLogged);
    });
})(window, window.angular)