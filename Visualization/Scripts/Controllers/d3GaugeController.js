VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.initialize = function(gauge)
  	{
        $scope.gauge = $scope.init.Gauges[gauge];
        
        $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };

        if($scope.gauge.DER != null){
            $scope.Method = "?host="+$scope.gauge.DER.Host+"&port="+$scope.gauge.DER.Port
                +"&wsInterface="+$scope.gauge.DER.Device+"&resource="+$scope.gauge.DER.Method;
        }else{
            $scope.Method = $scope.init.Method;
        }
        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.gauge.Host,$scope.gauge.Port,$scope.gauge.Device,$scope.Method)
                .success(function (response){
                $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID}, 
                    CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };

            });
        }, $scope.gauge.UpdateInterval);
    };
}]);
