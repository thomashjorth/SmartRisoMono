VisualizeApp.controller('configController', ['$scope','$interval', '$http', 'AppService', '$route', '$routeParams', '$location', function($scope, $interval, $http, AppService, $route, $routeParams, $location){
    $scope.params = $location.absUrl().split("?");
    if($scope.params.length > 1){
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
    $scope.current = 0;

    AppService.getData($scope.dataHost,$scope.dataPort,$scope.controller,$scope.config)
			.success(function (response){
				var config = JSON.parse(response);
				var slides = [];

				var include = [];
				var n = [];
				if($scope.slides == undefined){
					slides=config;
					for(var j = 0; j < slides.Pages.length; j ++){
						for(var i = 0; i < slides.Pages[j].Page.length; i ++){
							slides.Pages[j].Page[i].id="box"+(i+1);
							slides.Pages[j].Page[i].VisualizationType="Visualization/"+slides.Pages[j].Page[i].VisualizationType;
							n.push(slides.Pages[j].Page[i]);
						}
						n.Current=false;
						include.push(n);
						n = [];
					}
				}
				else{
					for (var i = 0; i < $scope.slides.length; i++) {
						slides.push(config.Pages[$scope.slides[i]]);
					}

					for(var j = 0; j < $scope.slides.length; j ++){
						for(var i = 0; i < slides[j].Page.length; i ++){
							slides[j].Page[i].id="box"+(i+1);
							slides[j].Page[i].VisualizationType="Visualization/"+slides[j].Page[i].VisualizationType;
							n.push(slides[j].Page[i]);
						}
						n.Current=false;
						include.push(n);
						n = [];
					}
				}
				include[$scope.current].Current = true;
					alert(JSON.stringify(include[$scope.current].Current));
				$scope.config = slides;
				$scope.include = include;
				$scope.loaded = true;

					alert(JSON.stringify($scope.include[$scope.current]));

				$interval(function(){ 
					$scope.include[0].Current = !$scope.include[0].Current;
					$scope.include[1].Current = !$scope.include[1].Current;
		        }, 30000);
    	});

}]);