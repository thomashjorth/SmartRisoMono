VisualizeApp.controller('sideBarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){

     $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
                if($scope.data.values.length > $scope.xTicks){
                    $scope.data.values.shift();
                }
                $scope.data.values.push({timestamp: h, value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.title, xTicks: $scope.xTicks, yMin: $scope.valueMin, yMax: $scope.valueMax}, values: $scope.data.values};
            });
    }, 1000);
}]);