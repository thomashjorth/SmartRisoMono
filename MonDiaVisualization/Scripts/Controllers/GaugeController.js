VisualizeApp.controller('GaugeController', function ($scope, $http, AppService) {
    
    drawGauge(-2, 2, "Power");
    //updateValue(-0.44);

   

    AppService.getMethodRealtime("getActivePower")
    	.success(function (response){
    		var json = JSON.parse(response);
    		alert(json);
    		updateValue(json);
    	});
	}
);
$inject = ['$scope'];
$inject = ['$http'];
$inject = ['AppService'];
