VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now());
	$scope.initialize = function()
  	{
        $scope.data={config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: [
            {timestamp: $scope.firstTime/1000,value: 0}
        ]};

        if($scope.init.DER != null){
            $scope.Resource = "?host="+$scope.init.DER.Host+"&port="+$scope.init.DER.Port
                +"&wsInterface="+$scope.init.DER.Aggregation+"&resource="+$scope.init.DER.Resource;
        }else{
            $scope.Resource = $scope.init.Resource;
        }
        alert($scope.Resource)
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.Resource)
                .success(function (response){

                $scope.data = {config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: [{timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value}]};
            });

        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.Resource)
                .success(function (response){
                if($scope.data.values.length > $scope.init.XTicks){
                    $scope.data.values.shift();
                }

                $scope.data.values.push({timestamp: JSON.parse(response).timestampMicros/1000, value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: $scope.data.values};
            });
        }, 1000);
  	};

}]);

