(function (window, angular) {
  'use-strict';
  angular.module('app', ['ui.router', 'mainModule'])
    .config(function ($urlRouterProvider, $locationProvider) {
      $urlRouterProvider.otherwise('/main/login');
      $locationProvider.hashPrefix('');
    })
    .run(function ($state) {
      $state.go('main.login');
    });
})(window, window.angular);