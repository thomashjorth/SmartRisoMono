VisualizeApp.controller('d3MultiGraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){

    $scope.run = true;
	$scope.initialize = function()
  	{
        $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
            values: []};
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
            .success(function (response){
            var json = JSON.parse(response);
            var temp = [];
            for (var i = 0; i < json.length; i++) {
                temp.push({name: ""+i, timestamp: json[i].timestampMicros/1000, value: json[i].value});
            }
            $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
            values: temp};
        });

        $interval(function(){
            var h=Math.floor(Date.now())-$scope.firstTime;
            if($scope.run){
                $scope.run = false;
                AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
                    .success(function (response){
                    var json = JSON.parse(response);
                    if($scope.data.values.length > $scope.init.XLength){
                        for (var i = 0; i < json.length; i++) {
                            $scope.data.values.shift();
                        };
                    }
                    
                    for (var i = 0; i < json.length; i++) {
                        $scope.data.values.push({name: ""+i, timestamp: json[i].timestampMicros/1000, value: json[i].value});
                    }
                    $scope.data = {config: {unit: $scope.init.Unit, yMin: $scope.init.ValueMin, yMax: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                    values: $scope.data.values};
                    $scope.run = true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, 1000);
  	};

}]);

