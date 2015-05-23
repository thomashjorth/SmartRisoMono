VisualizeApp.controller('unitController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    $scope.run=true;
    $scope.initialize = function()
    {
        $scope.dataHost = $scope.init.Host;
        $scope.dataPort = $scope.init.Port;
        $scope.dataAggregation = 'Aggregation';
        $scope.dataResource = $scope.init.Params;
        $scope.image = "Plug";
        var lastResponse;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
                $scope.Der = JSON.parse(response)
                $scope.size = (($(".state").height()/100)*75)/$scope.Der.Apps;
                lastResponse = response;
           });

        $interval(function(){
            if($scope.run){
                $scope.run=false;
            AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
                .success(function (response){
                    if(lastResponse != response){
                        $scope.Der = JSON.parse(response)
                        lastResponse = response;
                    }
                    $scope.run=true;
                });
            }
        }, 3000);
    };
}]);
