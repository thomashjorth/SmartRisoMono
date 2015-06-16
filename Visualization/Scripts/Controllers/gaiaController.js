VisualizeApp.controller('gaiaController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
	$scope.temp = $scope.init;
    if($scope.init.length != undefined){
        $scope.temp=$scope.init.shift();
    }

	$scope.params = $scope.temp.Params + "&resource="

		//$scope.messages();
	$interval(function(){
		$scope.messages();
	}, 10000);

	$scope.commandUnit = function (command) {
		AppService.putData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"Put/"+$scope.params+ command)
			.success(function (res) {	
				$scope.messages();
		})
		.error(function (error) {
			$scope.error = "Error using " + command;
		});
	}


	$scope.messages = function () {
		AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"GetStatus/"+$scope.params+ "getTurbineStatus")
			.success(function (response){
				var result = JSON.parse(response)
				if(result.status != undefined)
					$scope.message = "Current status is " + result.status + ".";
				else
					$scope.message = "Error getting status";
			});
	}
}]);
