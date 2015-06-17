VisualizeApp.controller('dieselController', ['$scope', '$element','$interval', '$http', 'AppService', function($scope, $element,$interval, $http, AppService){

	$scope.temp = $scope.init;
	$scope.message = "";
    if($scope.init.length != undefined){
            $scope.temp=$scope.init[$element[0].parentNode.attributes['value'].value]
        }
	$scope.params = $scope.temp.Params + "&resource="


	$scope.commandUnit = function (command) {
		AppService.putData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,"Put/"+$scope.params+ command)
			.success(function (res) {	
				$scope.messages = "Success";
			})
			.error(function (error) {
				$scope.messages = "Error";
			});
	}
}]);
