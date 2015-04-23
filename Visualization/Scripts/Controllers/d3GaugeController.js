VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.initialize = function(gauge)
  	{
        $scope.gauge = $scope.init.Gauges[gauge];
        
        $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };

        $scope.Resource = "?host="+$scope.gauge.DER.Host+"&port="+$scope.gauge.DER.Port
            +"&wsInterface="+$scope.gauge.DER.Aggregation+"&resource="+$scope.gauge.DER.Resource;
    
        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.gauge.Host,$scope.gauge.Port,$scope.gauge.Aggregation,$scope.Resource)
                .success(function (response){
                    alert(response);
                $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID}, 
                    CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };

            });
        }, 1000);
    };
}]);