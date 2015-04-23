VisualizeApp.controller('sideBarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    $scope.init = function(host, port, aggregation, resource)
    {
        $scope.dataHost = host;
        $scope.dataPort = port;
        $scope.dataAggregation = aggregation;
        $scope.dataResource = resource;

        $scope.image = "Plug";
        var lastResponse;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
                $scope.Ders = JSON.parse(response)
                lastResponse = response;
           });


        $interval(function(){
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){

            if(lastResponse != response){
                $scope.Ders = JSON.parse(response)
                lastResponse = response;
                }
           });
        }, 3000);
    };
}]);
