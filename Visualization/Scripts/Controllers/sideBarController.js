VisualizeApp.controller('sideBarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
$scope.init = function(host, port, aggregation, resource)
    {
        $scope.dataHost = host;
        $scope.dataPort = port;
        $scope.dataAggregation = aggregation;
        $scope.dataResource = resource;

<<<<<<< Upstream, based on origin/master
    };



    $scope.image = "Plug";
     $interval(function(){
     var lastResponse;
        AppService.getData($scope.dataHost,$scope.dataPort,$scope.dataAggregation,$scope.dataResource)
            .success(function (response){

            if(lastResponse != response){
            	$scope.Ders = JSON.parse(response)
        		lastResponse = response;
            	}
           });
    }, 3000);
=======
>>>>>>> bd5874a * Site.css: * test.dll.mdb: * Visualization.csproj: * test.dll: * Index.cshtml: * test.dll.mdb: * d3Pie.cshtml: * d3Bar.cshtml: * config.cshtml: * d3Graph.cshtml: * d3Gauge.cshtml: * d3GaugeDirective.js: * VisualizationController.cs: * d3PieController.js: * d3BarController.js: * configController.js: * d3GaugeController.js: * sideBarController.js: * d3GraphController.js: * Visualization.csproj.FilesWrittenAbsolute.txt: 
}]);
