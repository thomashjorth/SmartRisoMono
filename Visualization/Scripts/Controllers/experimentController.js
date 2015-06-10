VisualizeApp.controller('experimentController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, experimentDirective){

    $scope.run = true;
	$scope.initialize = function(config)
  	{
		$scope.Hosts = "";
		$scope.Devices = "";
		$scope.Resources = "";
    $scope.Units = [];
    $scope.init = [];
  		for(var i = 0; i < config.Units.length; i++){
  			$scope.Hosts += config.Units[i].Host + ":" + config.Units[i].Port + ";";
  			$scope.Devices += config.Units[i].Device +";";
  			$scope.Resources += "get"+config.Units[i].Params +";";
  			$scope.Units.push({Id: config.Units[i].Id, 
  				Visualization1: config.Units[i].Visualization1, 
  				Visualization2: config.Units[i].Visualization2,
          Host: config.HostAgg,
          Port: config.PortAgg,
          Device: config.Units[i].Device.substring(0, config.Units[i].Device.length - 2),
          Method: config.Units[i].Params})
  		}

		$scope.Hosts = $scope.Hosts.substring(0, $scope.Hosts.length - 1);
		$scope.Devices = $scope.Devices.substring(0, $scope.Devices.length - 1);
		$scope.Resources = $scope.Resources.substring(0, $scope.Resources.length - 1);

  		$scope.data = {config: {units: $scope.Units, 
            values: []}};
        
        $scope.Params = "GetUnits/?hosts="+$scope.Hosts+"&wsInterfaces="+$scope.Devices+"&resources="+$scope.Resources

        AppService.getData(config.HostAgg,config.PortAgg,"Aggregation",$scope.Params)
            .success(function (response){
            	var json = JSON.parse(response);
            	
            	$scope.data = {config: {units: $scope.Units}, 
            		values: json};
        });

        $interval(function(){
            if($scope.run){
                $scope.run = false;
        	AppService.getData(config.HostAgg,config.PortAgg,"Aggregation",$scope.Params)
	            .success(function (response){
	            	var json = JSON.parse(response);
	            	
	            	$scope.data = {config: {units: $scope.Units}, 
	            		values: json};
                    $scope.run = true;
        		})
                .error( function (response){
                    $scope.run=true;
                });  
            }
        }, 10000);
    };
}]);
