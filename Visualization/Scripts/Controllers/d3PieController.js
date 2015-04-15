VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.LabeledInstance = [
        {label: 'Star Wars', value: 27, color: "#98abc5"},
        {label: 'Lost In Space', value: 3, color: "#8a89a6"},
        {label: 'the Boston Pops', value: 20, color: "#7b6888"},
        {label: 'Indiana Jones', value: 15, color: "#6b486b"},
        {label: 'Potter', value: 7, color: "#a05d56"},
        {label: 'Jaws',  value: 5, color: "#d0743c"},
        {label: 'Lincoln', value: 2, color: "#ff8c00"}
    ];

    $interval(function(){
        AppService.getMethodAggregation("AllActivePower")
            .success(function (response){
                $scope.LabeledInstance = JSON.parse(LabeledInstance);

            });
    }, 1000);
}]);