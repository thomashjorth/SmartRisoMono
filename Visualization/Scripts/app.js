var VisualizeApp = angular.module('VisualizeApp', ['ngRoute']);


var configFunction = function ($routeProvider) {

    $routeProvider.
        when('/chart/gauge', {
            templateUrl: 'visualization/gauge',
            controller: 'GaugeController'
        })
        .when('/chart/d3graph', {
            templateUrl:'visualization/d3graph',
            controller: 'd3GraphController'
        });
}
configFunction.$inject = ['$routeProvider'];

VisualizeApp.config(configFunction);