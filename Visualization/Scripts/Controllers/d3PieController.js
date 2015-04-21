VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.init = function(host, port, aggregation, resource, titleHeading, valueMin, valueMax)
    {

        $scope.dataHost = host;
        $scope.dataPort = port;
        $scope.dataAggregation = aggregation;
        $scope.dataResource = resource;
        $scope.title = titleHeading;
        $scope.valueMin = valueMin;
        $scope.valueMax = valueMax;


    };



    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){

                $scope.data =
                {config: {label: $scope.title}, LabeledInstance: JSON.parse(response) };

            });
    }, 1000);
}]);