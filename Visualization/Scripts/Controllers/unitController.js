VisualizeApp.controller('unitController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    $scope.initialize = function()
    {
        $scope.dataHost = $scope.init.Host;
        $scope.dataPort = $scope.init.Port;
        $scope.dataAggregation = 'Aggregation';
        $scope.dataResource = $scope.init.DER;
        $scope.image = "Plug";

        var lastResponse;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
                $scope.Der = JSON.parse(response)
                lastResponse = response;
           });

        $interval(function(){
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
            if(lastResponse != response){
                $scope.Der = JSON.parse(response)
                lastResponse = response;
                }
           });
        }, 3000);
    };
}]);
