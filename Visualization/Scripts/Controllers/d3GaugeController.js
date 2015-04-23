VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);

	$scope.initialize = function()
  	{

        $scope.data={config: {label: $scope.init.TitleHeading, min: $scope.init.ValueMin, max: $scope.init.ValueMax, PlaceHolder: $scope.init.ID},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };
    
    };


    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.init.Resource)
			.success(function (response){

	    		$scope.data ={config: {label: $scope.init.TitleHeading, min: $scope.init.ValueMin, max: $scope.init.ValueMax, PlaceHolder: $scope.init.ID}, CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };

    	});
    }, 1000);
}]);