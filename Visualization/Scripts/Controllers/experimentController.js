VisualizeApp.controller('experimentController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, experimentDirective){

    $scope.run = true;
	$scope.initialize = function()
  	{
  		for(int i = 0; i < $scope.init.Units.length; i ++){
  			$scope.Hosts += $scope.init.Units[i].Host + ":" + $scope.init.Units[i].Port + ";";
  			$scope.Devices += $scope.init.Units[i].Device +";";
  			$scope.Resources += $scope.init.Units[i].Param +";";

  			$scope.Units.push({Id: $scope.init.Units[i].Id, 
  				Visualization1: $scope.init.Units[i].Visualization1, 
  				Visualization2: $scope.init.Units[i].Visualization2})
  		}

  		$scope.data = {config: {units: $scope.Units, 
            values: []};
        
        $scope.Params = "GetUnits/?hosts="+$scoper.Hosts+"&wsInterfaces="+$scoper.Devices+"&resources="+$scoper.Resources

        AppService.getData($scope.init.HostAgg,$scope.init.PortAgg,"Aggregator",$scope.Params)
            .success(function (response){
            var json = JSON.parse(response);
            var temp = [];
            for (var i = 0; i < json.length; i++) {
                temp.push({name: json[i].label, timestamp: json[i].measurement.timestampMicros/1000, value: json[i].measurement.value});
            }
            $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
            values: temp};
        });

        $interval(function(){

        }, 10000);
    };
}]);
