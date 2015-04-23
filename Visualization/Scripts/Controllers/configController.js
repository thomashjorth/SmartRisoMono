VisualizeApp.controller('configController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    
    $scope.dataHost = "localhost";
    $scope.dataPort = "9001";
    //$scope.controller = "Realtime";
    //$scope.config = "getActivePower";
    $scope.controller = "PageConfiguration";
    $scope.config = "singlePageAll";

    AppService.getData($scope.dataHost,$scope.dataPort,$scope.controller,$scope.config)
			.success(function (response){
				var config = JSON.parse(response);

				var include = [];

				for(var i = 0; i < config.Pages[0].Page.length; i ++){
					config.Pages[0].Page[i].id="box"+(i+1);
					config.Pages[0].Page[i].VisualizationType="Visualization/"+config.Pages[0].Page[i].VisualizationType;
					include.push(config.Pages[0].Page[i]);
				}
				$scope.include = include;
				//$scope.page = include[0];
    	});

	$scope.test = function(){
		return "test";
	}
}]);