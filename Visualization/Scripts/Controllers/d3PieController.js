VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.labelInstance = [
        {label: '.',instances: 0}
    ];

    $interval(function(){
        AppService.getMethodAggregation("AllPosActivePower")
            .success(function (response){
                $scope.labelInstance =JSON.parse(response);
            });
    }, 1000);
}]);