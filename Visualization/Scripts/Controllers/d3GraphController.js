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
        $scope.yMin = ymin;
        $scope.yMax = ymax;

        $scope.data={config: {unit: $scope.title}, values: [
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
                $scope.data.values.push({timestamp: h, value: Math.random()-0.5});
                $scope.data = {config: {unit: $scope.title}, values: $scope.data.values};
            });
    }, 1000);

}]);

