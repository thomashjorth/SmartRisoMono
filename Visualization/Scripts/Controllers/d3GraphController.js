VisualizeApp.controller('d3GraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){
	$scope.firstTime = Math.floor(Date.now()/1000);
	$scope.initialize = function()
  	{
        $scope.data={config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: [
            {timestamp: new Date(),value: 0}
        ]};

        if($scope.init.DER != null){
            $scope.Resource = "?host="+$scope.init.DER.Host+"&port="+$scope.init.DER.Port
                +"&wsInterface="+$scope.init.DER.Aggregation+"&resource="+$scope.init.DER.Resource;
        }else{
            $scope.Resource = $scope.init.Resource;
        }

        $interval(function(){
<<<<<<< Upstream, based on origin/master

          
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.init.Resource)
=======
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.Resource)
>>>>>>> 0e62d72 sd
                .success(function (response){
                if($scope.data.values.length > $scope.xTicks){
                    $scope.data.values.shift();
                }

                $scope.data.values.push({timestamp: new Date(JSON.parse(response).timestampMicros/1000), value: JSON.parse(response).value});
                $scope.data = {config: {unit: $scope.init.TitleHeading, xTicks: $scope.init.XTicks, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax}, values: $scope.data.values};
            });
        }, 1000);
  	};

}]);

