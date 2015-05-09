VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now());
	$scope.initialize = function()
  	{
        $scope.data={config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: [
            {timestamp: $scope.firstTime/1000,value: 0}
        ]};

        $scope.Method = $scope.init.DER;

        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Method)
                .success(function (response){

                $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: [{timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value}]};
            });

        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.Method)
                .success(function (response){
                if($scope.data.values.length > $scope.init.XLength){
                    $scope.data.values.shift();
                }

                $scope.data.values.push({timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, values: $scope.data.values};
            });
        }, $scope.init.UpdateInterval);
  	};

}]);

