VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.init = function(host, port, aggregation, resource, titleHeading,xticks, ymin, ymax)
  	{
  		$scope.dataHost = host;
        $scope.dataPort = port;
        $scope.dataAggregation = aggregation;
        $scope.dataResource = resource;
        $scope.title = titleHeading;
        $scope.xTicks = xticks;
        $scope.valueMin = ymin;
        $scope.valueMax = ymax;

        $scope.data={config: {unit: titleHeading, xTicks: xticks, yMin: ymin, yMax: ymax}, values: [
            {timestamp: 0,value: 0}
        ]};
  	};


    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){
                if($scope.data.values.length > $scope.xTicks){
                    $scope.data.values.shift();
                }
                $scope.data.values.push({timestamp: h, value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.title, xTicks: $scope.xTicks, yMin: $scope.yMin, yMax: $scope.yMax}, values: $scope.data.values};
            });
    }, 1000);

}]);

