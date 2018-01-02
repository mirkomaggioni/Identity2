(function (window, angular) {
  'use-strict';
  angular.module('mainModule', ['ui.router', 'authenticationModule'])
    .config(function ($stateProvider) {
      var mainState = {
        name: 'main',
        url: '/main',
        views: {
          'header': { templateUrl: 'app/main/header.html' },
          'main': { templateUrl: 'app/main/main.html' }
        }
      }

      $stateProvider.state(mainState);
    })
})(window, window.angular)