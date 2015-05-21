VisualizeApp.controller('d3MultiGraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now());

    $scope.min = 0;
    $scope.max = 0;
    $scope.sum = 0;
    $scope.count = 0;

	$scope.initialize = function()
  	{
        $scope.data={config: {unit: "multi", yMin: -2, yMax: 2, title: "test"},
        values: [
            {name: "1", timestamp: $scope.firstTime,value: 0},
            {name: "2", timestamp: $scope.firstTime,value: 1}
        ]};
        //$scope.Params = $scope.init.Params;

        /*AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Params)
                .success(function (response){
                var result = JSON.parse(response);

                $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                values: [{timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value}]};
            });
*/
        $interval(function(){
            var h=Math.floor(Date.now())-$scope.firstTime;
            //AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Params)
            //    .success(function (response){
                if($scope.data.values.length > 20){
                    $scope.data.values.shift();
                    $scope.data.values.shift();
                }
                //alert(Math.random())
                $scope.data.values.push({name: "L1", timestamp: Math.floor(Date.now()), value: Math.random()});
                $scope.data.values.push({name: "L2", timestamp: Math.floor(Date.now()), value: Math.random()});
                $scope.data = {config: {unit: "multi", yMin: -2, yMax: 2, title: "test"}, values: $scope.data.values};
            //});
        }, 1000);
  	};

}]);

