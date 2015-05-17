VisualizeApp.controller('tasksController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){


	$scope.params = $scope.init.Params + "&resource="


	$interval(function(){
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
	}, 10000);

	$scope.startUnit = function () {
		AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,"Put/"+$scope.params+ "startLoad")
			.success(function (res) {
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
		});
	}

	$scope.stopUnit = function () {
		AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,"Put/"+$scope.params+"stopLoad")
			.success(function (res) {
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
		});
	}

}]);
