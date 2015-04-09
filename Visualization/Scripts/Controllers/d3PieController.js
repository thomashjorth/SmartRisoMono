VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.labelInstance = [
        {label: 'Star Wars',instances: 207},
        {label: 'Lost In Space',instances: 3},
        {label: 'the Boston Pops',instances: 20},
        {label: 'Indiana Jones',instances: 150},
        {label: 'Potter',instances: 75},
        {label: 'Jaws', instances: 5},
        {label: 'Lincoln',instances: 1}
    ];

    $interval(function(){
        AppService.getMethodAggregation("AllActivePower")
            .success(function (response){
                $scope.labelInstance =JSON.parse(response);

            });
    }, 1000);
}]);