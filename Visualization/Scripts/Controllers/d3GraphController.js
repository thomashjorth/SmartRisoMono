VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, linearChart){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.init = function(length, method, controller)
  	{
        $scope.chartLength = length;
        $scope.AggregationMethod = method;
        $scope.controller = controller;
  	};

    $scope.data={config: {unit: "test"}, values: [
        {timestamp: 0,value: 0}
    ]};

    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        if($scope.data.values.length > $scope.chartLength){
            $scope.data.values.shift();
        }
        $scope.data.values.push({timestamp: h, value: Math.random()-0.5});
        $scope.data = {config: {unit: "test"}, values: $scope.data.values};
    }, 1000);
}]);

