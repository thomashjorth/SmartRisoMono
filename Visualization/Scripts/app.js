var VisualizeApp = angular.module('VisualizeApp', ['ngRoute']);


var configFunction = function ($routeProvider) {

    $routeProvider.
        when('/chart/gauge', {
            templateUrl: 'visualization/gauge',
            controller: 'GaugeController'
        })
        .when('/chart/graph', {
            templateUrl:'visualization/graph',
            controller: 'GraphController'
        });
}
configFunction.$inject = ['$routeProvider'];

VisualizeApp.config(configFunction);