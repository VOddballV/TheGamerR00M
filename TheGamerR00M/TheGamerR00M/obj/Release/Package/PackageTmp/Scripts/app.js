var app = angular.module('newSite', ['ui.router', 'ngAnimate', 'infinite-scroll', 'ui.bootstrap']);
app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', function ($stateProvider, $urlRouterProvider, $locationProvider) {
    var rootUrl = $("#linkRoot").attr("href");
    $urlRouterProvider.otherwise('/Oops');
    $stateProvider
        .state('Home', {
            url: '/home',
            templateUrl: 'App/Views/Home/index.cshtml',
    })
    $stateProvider
    .state('Default', {
        url: '/',
        templateUrl: 'App/Views/Home/index.cshtml',
    })
    $stateProvider
        .state('Default2', {
            url: '#!/',
            templateUrl: 'App/Views/Home/index.cshtml'
    });
    $stateProvider
        .state('Reviews', {
            url: '/reviews',
            templateUrl: 'App/Views/Reviews/index.cshtml'
        });
    //$stateProvider
    //    .state('PageNotFound', {
    //        url: '/*.',
    //        templateUrl: 'PageNotFound.cshtml'
    //    });
    //$stateProvider
    //    .state('Default', {
    //        url: '/',
    //        templateUrl: '_Layout.cshtml'
    //    });
    $stateProvider
        .state('Oops', {
        url: '/Oops',
        templateUrl: 'App/Views/PageNotFound.cshtml'
    });
    $locationProvider.hashPrefix('!');
}]);

app.controller('MainController', ['$scope', '$Window', function ($scope, $Window) {

}]);

app.directive('loading', ['$http', function ($http) {
    return {
        restrict: 'A',
        link: function (scope, elm, attrs) {
            scope.isloadingToday = function () {
                return $http.pendingRequests.length > 0;
            };

            scope.$watch(scope.isloadingToday, function (v) {
                if (v) {
                    elm.show();
                } else {
                    elm.fadeOut(1000);
                }
            });
        }
    };
}]);
