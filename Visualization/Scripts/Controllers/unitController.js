VisualizeApp.controller('unitController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    $scope.run=true;
    $scope.initialize = function()
    {
        $scope.temp = $scope.init;
        if($scope.init.length != undefined){
            $scope.temp=$scope.init.shift();
        }
        $scope.dataHost = $scope.temp.Host;
        $scope.dataPort = $scope.temp.Port;
        $scope.dataAggregation = 'Aggregation';
        $scope.dataResource = $scope.temp.Params;
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
