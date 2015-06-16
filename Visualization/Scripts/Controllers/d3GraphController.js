VisualizeApp.controller('d3GraphController', ['$scope', '$element', '$interval', '$http', 'AppService', function($scope, $element, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now());

    $scope.min = 0;
    $scope.max = 0;
    $scope.sum = 0;
    $scope.count = 0;
    $scope.run = true;
	$scope.initialize = function()
  	{
        //alert($scope.init.length)
        $scope.temp = $scope.init;
        if($scope.init.length != undefined){
            $scope.temp=$scope.init[$element[0].parentNode.attributes['value'].value];
            //alert(JSON.stringify($scope.temp))
        }
        $scope.data={config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, values: [
            {timestamp: $scope.firstTime/1000,value: 0}
        ]};
        $scope.Params = $scope.temp.Params;

        AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.Params)
                .success(function (response){
                var result = JSON.parse(response);
                $scope.min = result.value;
                $scope.max = result.value;
                $scope.sum = result.value;
                $scope.count = 1;

                $scope.data = {config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, values: [{timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value}], statistics: {min: $scope.min, max: $scope.max, avg: $scope.sum/$scope.count}};
            });

        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.Params)
                    .success(function (response){
                    if($scope.data.values.length > $scope.temp.XLength){
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
                    $scope.data = {config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, values: $scope.data.values, 
                        statistics: {min: $scope.min, max: $scope.max, avg: $scope.sum/$scope.count}};
                    $scope.run=true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.temp.UpdateInterval);
  	};

}]);

