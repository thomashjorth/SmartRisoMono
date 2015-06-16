VisualizeApp.controller('dumploadController', ['$scope', '$element','$interval', '$http', 'AppService', function($scope, $element,$interval, $http, AppService){

	$scope.temp = $scope.init;
    if($scope.init.length != undefined){
            $scope.temp=$scope.init[$element[0].parentNode.attributes['value'].value]
        }
	$scope.params = $scope.temp.Params + "&resource="

	$interval(function(){
		$scope.messages();
	}, 10000);

	$scope.commandUnit = function (command) {
		AppService.putData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"Put/"+$scope.params+ command)
			.success(function (res) {	
				$scope.messages();
			})
			.error(function (error) {
				$scope.messages();
			});
	}


	$scope.messages = function () {
		AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"GetCompositeBoolean/"+$scope.params+ "isFanRunning")
			.success(function (response){
				var result = JSON.parse(response)
				if(result.value)
					$scope.dumpload = "Dumpload is on.";
				else
					$scope.dumpload = "Dumpload is off.";
			})
			.error(function (error) {
				$scope.dumpload = "Error checking status of dumpload.";
			});

		AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"GetCompositeBoolean/"+$scope.params+ "isLoadOn")
			.success(function (response){
				var result = JSON.parse(response)
				if(result.value)
					$scope.fan = "Fan is running."
				else
					$scope.fan = "Fan is not running."
			})
			.error(function (error) {
				$scope.fan = "Error checking status of fan.";
			});
	}
}]);
