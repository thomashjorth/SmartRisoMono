VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.initialize = function()
  	{
        $scope.data={config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: [
            {timestamp: 0,value: 0}
        ]};

        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.init.Resource)
                .success(function (response){
                if($scope.data.values.length > $scope.xTicks){
                    $scope.data.values.shift();
                }
                $scope.data.values.push({timestamp: h, value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: $scope.data.values};
            });
        }, 1000);
  	};

}]);

