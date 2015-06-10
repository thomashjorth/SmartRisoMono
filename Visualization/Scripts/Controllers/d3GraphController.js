VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now());

    $scope.min = 0;
    $scope.max = 0;
    $scope.sum = 0;
    $scope.count = 0;
    $scope.run = true;
	$scope.initialize = function()
  	{
        $scope.data={config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: [
            {timestamp: $scope.firstTime/1000,value: 0}
        ]};
        $scope.Params = $scope.init.Params;

        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Params)
                .success(function (response){
                var result = JSON.parse(response);
                $scope.min = result.value;
                $scope.max = result.value;
                $scope.sum = result.value;
                $scope.count = 1;

                $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: [{timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value}], statistics: {min: $scope.min, max: $scope.max, avg: $scope.sum/$scope.count}};
            });

        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Params)
                    .success(function (response){
                    if($scope.data.values.length > $scope.init.XLength){
                        $scope.data.values.shift();
                    }
                    var result = JSON.parse(response);
                    if($scope.min > result.value)
                        $scope.min = result.value;
                    else if($scope.max < result.value)
                        $scope.max = result.value;
                    $scope.sum += result.value;
                    $scope.count += 1;
                    $scope.data.values.push({timestamp: result.timestampMicros/1000, value: result.value});
                    $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: $scope.data.values, 
                        statistics: {min: $scope.min, max: $scope.max, avg: $scope.sum/$scope.count}};
                    $scope.run=true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.init.UpdateInterval);
  	};

}]);

