VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, linearChart){
	$scope.firstTime = Math.floor(Date.now()/1000);
    $scope.salesData=[
        {hour: 0,sales: 0}
    ];

    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getMethodAggregation("AvgActivePower")
    		.success(function (response){
    		if($scope.salesData.length > 10){
    			$scope.salesData.shift();
    			//alert($scope.salesData[$scope.salesData.length-1].hour + " " + $scope.salesData[$scope.salesData.length-1].sales);
    		}
    		$scope.salesData.push({hour: h, sales:JSON.parse(response)});
    	});
    }, 1000);
}]);