VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.init = function(host, port, aggregation, resource, titleHeading, valueMin, valueMax)
  	{
  
  		$scope.dataHost = host;
        $scope.dataPort = port;
        $scope.dataAggregation = aggregation;
        $scope.dataResource = resource;
        $scope.title = titleHeading;
        $scope.valueMin = valueMin;
        $scope.valueMax = valueMax;

        $scope.data={config: {label: $scope.title, min: $scope.valueMin, max: $scope.valueMax},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };
    
    };


    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
			.success(function (response){

	    		$scope.data ={config: {label: $scope.title, min: $scope.valueMin, max: $scope.valueMax}, CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };

    	});
    }, 1000);
}]);