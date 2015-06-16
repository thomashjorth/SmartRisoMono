VisualizeApp.controller('d3MultiGraphController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3GraphDirective){

    $scope.run = true;
	$scope.initialize = function()
  	{
        $scope.temp = $scope.init;
        if($scope.init.length != undefined){
            $scope.temp=$scope.init.shift();
        }
        $scope.data = {config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, 
            values: []};
        AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
            .success(function (response){
            var json = JSON.parse(response);
            var temp = [];
            for (var i = 0; i < json.length; i++) {
                temp.push({name: json[i].label, timestamp: json[i].measurement.timestampMicros/1000, value: json[i].measurement.value});
            }
            $scope.data = {config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, 
            values: temp};
        });

        $interval(function(){
            var h=Math.floor(Date.now())-$scope.firstTime;
            if($scope.run){
                $scope.run = false;
                AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
                    .success(function (response){
                    var json = JSON.parse(response);
                    if($scope.data.values.length > $scope.temp.XLength){
                        for (var i = 0; i < json.length; i++) {
                            $scope.data.values.shift();
                        };
                    }
                    
                    for (var i = 0; i < json.length; i++) {
                        $scope.data.values.push({name: json[i].label, timestamp: json[i].measurement.timestampMicros/1000, value: json[i].measurement.value});
                    }
                    $scope.data = {config: {unit: $scope.temp.Unit, yMin: $scope.temp.ValueMin, yMax: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, 
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

