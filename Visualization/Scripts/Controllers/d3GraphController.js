VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, linearChart){
    $scope.salesData=[
        {hour: 0,sales: 0}
    ];

    $interval(function(){
        var h=$scope.salesData.length+1;
        AppService.getMethodAggregation("AvgActivePower")
    		.success(function (response){
    		$scope.salesData.push({hour: h, sales:JSON.parse(response)});
    	});
    }, 1000);
}]);