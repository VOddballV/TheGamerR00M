var app = angular.module('newSite', ['ui.router', 'ngAnimate', 'infinite-scroll', 'ui.bootstrap']);
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');
    $stateProvider
        .state('Users', {
            url: '/Users',
            templateUrl: 'Users.html',
            controller: 'AddUserController'
        })
    $stateProvider
        .state('InstallForum', {
            url: '/InstallForum',
            templateUrl: 'Forum/phpBB3/install/index.php',
        });
}]);

app.controller('AddUserController', ['$scope', function ($scope) {

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
