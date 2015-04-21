VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);

    $scope.data={config: {label: "test", min: -1, max: 1}, CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };

    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getMethodRealtime("getActivePower")
			.success(function (response){
				
	    		$scope.data ={config: {label: "test", min: -1, max: 1}, CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };

    	});
    }, 1000);
}]);