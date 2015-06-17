VisualizeApp.controller('configController', ['$scope','$interval', '$http', 'AppService', '$route', '$routeParams', '$location', function($scope, $interval, $http, AppService, $route, $routeParams, $location){
    $scope.params = $location.absUrl().split("?");
    $scope.config = "undefined";
    $scope.server = "localhost"
    $scope.port = "9001"
    if($scope.params.length > 1){
    	$scope.params = $scope.params[1].split("&")
    	$scope.slides = [];
	    for (var i = 0; i < $scope.params.length; i++) {
	    	var p = $scope.params[i].split("=");
	    	if(p[0] == "slide"){
	    		$scope.slides.push(parseInt(p[1]));
	    	}
	    	else if(p[0] == "config"){
    			$scope.config = p[1];
	    	}
	    	else if(p[0] == "server"){
    			$scope.server = p[1];
	    	}
	    	else if(p[0] == "port"){
    			$scope.port = p[1];
	    	}
	    }
	}

    $scope.dataHost = $scope.server;//"localhost";//"192.168.0.74";
    $scope.dataPort = $scope.port;//"9001";//"8080";
    $scope.controller = "PageConfiguration/Get";
    $scope.current = 0;
    $scope.paused = false;
    AppService.getData($scope.dataHost,$scope.dataPort,$scope.controller,$scope.config)
			.success(function (response){
				var config = JSON.parse(response);
				var slides = [];

				var include = [];
				var n = [];
				if($scope.slides == undefined || $scope.slides.length == 0){
					slides=config;
					for(var j = 0; j < slides.Pages.length; j ++){
						slides.Pages[j].PageType="Views/Pages/"+slides.Pages[j].PageType +".html";
						if(slides.Pages[j].PageType == "Views/Pages/Grid3x3.html"){
							for(var i = 0; i < slides.Pages[j].Page.length; i ++){
								slides.Pages[j].Page[i].id="box"+(i+1);
								slides.Pages[j].Page[i].VisualizationType="Views/Visualization/"+slides.Pages[j].Page[i].VisualizationType +".html";
								n.push(slides.Pages[j].Page[i]);
							}
						}else{
							n = slides.Pages[j];
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
						slides.Pages[j].PageType="Views/Pages/"+slides.Pages[j].PageType +".html";
						if(slides.Pages[j].PageType == "Views/Pages/Grid3x3.html"){
							for(var i = 0; i < slides.Pages[j].Page.length; i ++){
								slides.Pages[j].Page[i].id="box"+(i+1);
								slides.Pages[j].Page[i].VisualizationType="Views/Visualization/"+slides.Pages[j].Page[i].VisualizationType +".html";
								n.push(slides.Pages[j].Page[i]);
							}
						}else if(slides.Pages[j].PageType == "Views/Pages/Experiment.html"){
							n = slides.Pages[j];
						}
						n.Current=false;
						include.push(n);
						n = [];
					}
				}
				include[$scope.current].Current = true;
				$scope.config = slides;
				$scope.include = include;
				$("#slide"+$scope.current).css("fontSize", "x-large");
				if(include.length > 1){
					$interval(function(){
						if($scope.paused == false){
							$scope.include[$scope.current].Current = false;
							$("#slide"+$scope.current).css("fontSize", "medium");

							if($scope.current == $scope.include.length-1){
								$scope.current = 0;
							}
							else
								$scope.current++;
							$scope.include[$scope.current].Current = true;
							$("#slide"+$scope.current).css("fontSize", "x-large");
						}
			        }, 30000);
				}
    	});
	

	$scope.controls = function(control) {
		if(control == "back"){
			$scope.include[$scope.current].Current = false;
			$("#slide"+$scope.current).css("fontSize", "medium");
			if($scope.current == 0)
				$scope.current = $scope.include.length-1;
			else
				$scope.current = $scope.current-1;
			$scope.include[$scope.current].Current = true;
			$("#slide"+$scope.current).css("fontSize", "x-large");
		}
		else if(control == "pause"){
			if($scope.paused){
				$scope.paused = false;
			}
			else{
				$scope.paused = true;
			}
		}
		else if(control == "next"){
			$scope.include[$scope.current].Current = false;
			$("#slide"+$scope.current).css("fontSize", "medium");
			if($scope.current == $scope.include.length-1)
				$scope.current = 0;
			else
				$scope.current = $scope.current+1;
			$scope.include[$scope.current].Current = true;
			$("#slide"+$scope.current).css("fontSize", "x-large");
		}
	};

	$scope.slide = function(index){
		$scope.include[$scope.current].Current = false;
		$("#slide"+$scope.current).css("fontSize", "medium");
		$scope.current = index;
		$scope.include[$scope.current].Current = true;
		$("#slide"+$scope.current).css("fontSize", "x-large");
	}


	document.addEventListener("keydown", keyDownTextField, false);

	function keyDownTextField(e) {
		var keyCode = e.keyCode;
		if (keyCode == 39) {
			$scope.controls('next');
		}
		else if(keyCode == 37){
			$scope.controls('back');
		}
		else if(keyCode == 32){
			$scope.controls('pause');
		}
	}
}]);
