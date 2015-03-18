VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, linearChart){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.init = function(length, method, controller)
  	{
    $scope.chartLength = length;
    $scope.AggregationMethod = method;
    $scope.controller = controller;
  	};

    $scope.values=[
        {timestamp: 0,value: 0}
    ];
    $scope.qualities=[
        {timestamp: 0,value: 0}
    ];
    $scope.timePrecisions=[
        {timestamp: 0,value: 0}
    ];
    $scope.validities=[
        {timestamp: 0,value: 0}
    ];
    $scope.sources=[
        {timestamp: 0,value: 0}
    ];


    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;

        if($scope.controller == 'Realtime'){
        AppService.getMethodRealtime($scope.method)
    		.success(function (response){

    		// values
    		if($scope.values.length > $scope.chartLength){
    			$scope.values.shift();
    			}
    		$scope.values.push({timestamp: h, value:JSON.parse(response).value});

    		// qualities
    		if($scope.qualities.length > $scope.chartLength){
    			$scope.qualities.shift();
    			}
    		$scope.qualities.push({timestamp: h, value:JSON.parse(response).quality});

    		// timePrecisions
    		if($scope.timePrecisions.length > $scope.chartLength){
    			$scope.qualities.shift();
    			}
    		$scope.timePrecisions.push({timestamp: h, value:JSON.parse(response).timePrecision});

    		// validities
    		if($scope.validities.length > $scope.chartLength){
    			$scope.validities.shift();
    			}
    		$scope.validities.push({timestamp: h, value:JSON.parse(response).validity});

    		// sources
    		if($scope.sources.length > $scope.chartLength){
    			$scope.sources.shift();
    			}
    		$scope.sources.push({timestamp: h, value:JSON.parse(response).source});

    	});
        }else{
        AppService.getMethodAggregation($scope.method)
    		.success(function (response){
    		// values
    		if($scope.values.length > $scope.chartLength){
    			$scope.values.shift();
    			}
    		$scope.values.push({timestamp: h, value:JSON.parse(response).value});

    	});
        }

    }, 1000);
}]);

