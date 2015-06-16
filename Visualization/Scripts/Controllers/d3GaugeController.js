VisualizeApp.controller('d3GaugeController', ['$scope','$element','$interval', '$http', 'AppService', function($scope, $element, $interval, $http, AppService, d3GaugeDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
    $scope.run = true;
	$scope.initialize = function()
  	{
        //$scope.init.shift();

        if($scope.init.length != undefined){
            $scope.temp=$scope.init[$element[0].parentNode.attributes['value'].value]
        }
        $scope.gauge = $scope.temp.Gauges[0];
        $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID, title: $scope.gauge.TitleHeading, green: $scope.gauge.Green, yellow: $scope.gauge.Yellow, red: $scope.gauge.Red},
            CompositeMeasurement: {v: 0,timestamp: $scope.firstTime} };

        $scope.Params = $scope.gauge.Params;
        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.gauge.Host,$scope.gauge.Port,$scope.gauge.Device,$scope.Params)
                    .success(function (response){
                    $scope.data={config: {label: $scope.gauge.Unit, min: $scope.gauge.ValueMin, max: $scope.gauge.ValueMax, PlaceHolder: $scope.gauge.ID, title: $scope.gauge.TitleHeading, green: $scope.gauge.Green, yellow: $scope.gauge.Yellow, red: $scope.gauge.Red}, 
                        CompositeMeasurement: {v: JSON.parse(response).value, timestamp: JSON.parse(response).timestampMicros/1000} };
                    $scope.run=true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.gauge.UpdateInterval);
    };
}]);
