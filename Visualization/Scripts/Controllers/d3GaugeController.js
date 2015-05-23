VisualizeApp.controller('d3GaugeController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
    $scope.run = true;
	$scope.initialize = function(gauge)
  	{
        $scope.gauge = $scope.init.Gauges[gauge];
        
        $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID, title: $scope.gauge.TitleHeading, green: $scope.gauge.Green, yellow: $scope.gauge.Yellow, red: $scope.gauge.Red},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };

        $scope.Params = $scope.gauge.Params;
        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.gauge.Host,$scope.gauge.Port,$scope.gauge.Device,$scope.Params)
                    .success(function (response){
                    $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID, title: $scope.gauge.TitleHeading, green: $scope.gauge.Green, yellow: $scope.gauge.Yellow, red: $scope.gauge.Red}, 
                        CompositeMeasurement: {v: JSON.parse(response).value, timestamp:h} };
                    $scope.run=true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.gauge.UpdateInterval);
    };
}]);
