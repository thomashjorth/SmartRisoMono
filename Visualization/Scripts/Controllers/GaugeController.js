VisualizeApp.controller('GaugeController', function ($scope, $http, AppService) {
    $.getScript("../Visualization/Gauge.js", function () { });

    drawGauge(-2, 2, "Power");
    updateValue(-0.44);

    AppService.getMethod("getActivePower")
    	.success(function (response){
    		alert(response);
    	});
	}
);
$inject = ['$scope'];
$inject = ['$http'];
$inject = ['AppService'];