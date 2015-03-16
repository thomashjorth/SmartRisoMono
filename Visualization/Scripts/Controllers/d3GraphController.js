VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, linearChart){
    $scope.salesData=[
        {hour: 0,sales: 0}
    ];

    $interval(function(){
        var hour=$scope.salesData.length+1;
        var sales= AppService.getMethodRealtime("AvgActivePower")
    		.success(function (response){
    	});
        $scope.salesData.push({hour: hour, sales:sales});
    }, 1000);
}]);