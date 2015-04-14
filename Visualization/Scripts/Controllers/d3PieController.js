VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.labelInstance = [
        {label: '.',instances: 1}
    ];

    $interval(function(){
        AppService.getMethodAggregation("AllPosActivePower")
            .success(function (response){
                if(JSON.parse(response).length > 0){
                    //this array is not empty
                    $scope.labelInstance =JSON.parse(response);
                }else{
                    //this array is empty
                }

            });
    }, 1000);
}]);