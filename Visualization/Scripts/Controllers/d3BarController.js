VisualizeApp.controller('d3BarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3BarDirective){
    $scope.LabeledInstance = [
        {label: 'Star Wars', value: 10},
        {label: 'Lost In Space', value: -28},
        {label: 'the Boston Pops', value: -2},
        {label: 'Indiana Jones', value: 5},
        {label: 'Potter', value: 7},
        {label: 'Jaws',  value: -5},
        {label: 'Lincoln', value: 15}
    ];

    $interval(function(){
        $scope.LabeledInstance = [
            {label: 'Star Wars', value: 27},
            {label: 'Lost In Space', value: 3},
            {label: 'the Boston Pops', value: -20},
            {label: 'Indiana Jones', value: 15},
            {label: 'Potter', value: -7},
            {label: 'Jaws',  value: -10},
            {label: 'Lincoln', value: 100}
        ];
    }, 1000);
}]);