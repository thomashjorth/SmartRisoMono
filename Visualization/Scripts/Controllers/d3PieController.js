VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){

    $scope.data = {config: {title: "test"}, LabeledInstance: [
        {label: 'Star Wars', value: 27},
        {label: 'Lost In Space', value: 3},
        {label: 'the Boston Pops', value: 20},
        {label: 'Indiana Jones', value: 15},
        {label: 'Potter', value: 7},
        {label: 'Jaws',  value: 5},
        {label: 'Lincoln', value: 2}
    ]};

    $interval(function(){
        AppService.getMethodAggregation("AllActivePower")
            .success(function (response){
                $scope.LabeledInstance = JSON.parse(LabeledInstance);

            });
    }, 1000);
}]);