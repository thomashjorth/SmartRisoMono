VisualizeApp.controller('dumploadController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){


	$scope.params = $scope.init.Params + "&resource="

	$interval(function(){
		$scope.messages();
	}, 10000);

	$scope.commandUnit = function (command) {
		AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,"Put/"+$scope.params+ command)
			.success(function (res) {	
				$scope.messages();
		});
	}

	$scope.stopUnit = function () {
		AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,"Put/"+$scope.params+command)
			.success(function (res) {
				$scope.messages();
		});
	}


	$scope.messages = function () {
		AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,"GetCompositeBoolean/"+$scope.params+ "isFanRunning")
			.success(function (response){
				var result = JSON.parse(response)
				if(result.value)
					$scope.dumpload = "Dumpload is on.";
				else
					$scope.dumpload = "Dumpload is off.";
			});

		AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,"GetCompositeBoolean/"+$scope.params+ "isLoadOn")
			.success(function (response){
			var result = JSON.parse(response)
			if(result.value)
				$scope.fan = "Fan is running."
			else
				$scope.fan = "Fan is not running."
		});
	}
}]);
