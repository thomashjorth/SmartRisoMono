VisualizeApp.controller('gaiaController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){


	$scope.params = $scope.init.Params + "&resource="

		//$scope.messages();
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
		AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,"GetStatus/"+$scope.params+ "getTurbineStatus")
			.success(function (response){
				var result = JSON.parse(response)
				if(result.status != undefined)
					$scope.message = "Current status is " + result.status + ".";
				else
					$scope.message = "Error getting status";
			});
	}
}]);
