var app = angular.module('myApp', ['ngRoute']);
app.config([
  '$locationProvider', '$routeProvider',
  function ($locationProvider, $routeProvider) {
      $locationProvider.html5Mode({
          enabled: true,
          requireBase: false
      }).hashPrefix('!');
      $routeProvider
      .when('/', { // For Home Page  
          templateUrl: '/FileUpload/Index.html',
          //controller: 'HomeController'
      })
      .when('/FileUpload/Index', { // For Contact page  
          templateUrl: '/FileUpload/Index.html',
          //controller: 'ContactController'
      })
      .when('/Download/Index', { // For About page  
          templateUrl: '/Download/Index.html',
          //controller: 'AboutController'
      })
      //.when('/Home/User/:userid', { // For User page   
      //    templateUrl: '/AngularTemplates/User.html',
      //    controller: 'UserController'
      //})
      .otherwise({  // This is when any route not matched => error  
          //controller: 'ErrorController'
      })
  }]);