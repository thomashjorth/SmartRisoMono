VisualizeApp.controller('configController', ['$scope','$interval', '$http', 'AppService', '$route', '$routeParams', '$location', function($scope, $interval, $http, AppService, $route, $routeParams, $location){
    $scope.params = $location.absUrl().split("?");
    if($scope.params.length > 1){
    	alert()
    	$scope.params = $scope.params[1].split("&")
    	$scope.slides = [];
	    for (var i = 0; i < $scope.params.length; i++) {
	    	var p = $scope.params[i].split("=");
	    	if(p[0] == "slide"){
	    		$scope.slides.push(parseInt(p[1]));
	    	}
	    }
	}

    $scope.dataHost = "localhost";
    $scope.dataPort = "9001";
    //$scope.controller = "Realtime";
    //$scope.config = "getActivePower";
    $scope.controller = "PageConfiguration";
    $scope.config = "sd";

    AppService.getData($scope.dataHost,$scope.dataPort,$scope.controller,$scope.config)
			.success(function (response){
				var config = JSON.parse(response);
				var slides = [];
				for (var i = 0; i < $scope.slides.length; i++) {
					slides.push(config.Pages[$scope.slides[i]]);
				}

				var include = [];
				if($scope.slides == undefined){
					for(var j = 0; j < slides.Pages.length; j ++){
						for(var i = 0; i < slides.Pages[j].Page.length; i ++){
							slides.Pages[j].Page[i].id="box"+(i+1);
							slides.Pages[j].Page[i].VisualizationType="Visualization/"+slides.Pages[j].Page[i].VisualizationType;
							include.push(slides.Pages[j].Page[i]);
						}
					}
				}
				else{
					for(var j = 0; j < $scope.slides.length; j ++){
						for(var i = 0; i < config.Pages[j].Page.length; i ++){
							config.Pages[j].Page[i].id="box"+(i+1);
							config.Pages[j].Page[i].VisualizationType="Visualization/"+config.Pages[j].Page[i].VisualizationType;
							include.push(config.Pages[j].Page[i]);
						}
					}
				}
				$scope.config = config;
				$scope.include = include;
				$scope.loaded = true;
    	});
	

	function in_array(array, id) {
    for(var i=0;i<array.length;i++) {
        if(array[i] == id)
        	return true;
    }
    return false;
}
}]);