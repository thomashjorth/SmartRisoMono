VisualizeApp.controller('d3BarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3BarDirective){
    $scope.run = true;
    $scope.initialize = function()
    {
        $scope.temp = $scope.init;
        if($scope.init.length != undefined){
            $scope.temp=$scope.init.shift();
        }

        AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
            .success(function (response){

                $scope.data = {config: {label: $scope.temp.Unit, min: $scope.temp.ValueMin, max: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, 
                    LabeledInstance: JSON.parse(response) };

            });
        $interval(function(){
            if($scope.run){
                $scope.run = false;
                AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
                    .success(function (response){
                    $scope.data = {config: {label: $scope.temp.Unit, min: $scope.temp.ValueMin, max: $scope.temp.ValueMax, title: $scope.temp.TitleHeading}, 
                        LabeledInstance: JSON.parse(response) };
                    $scope.run = true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.temp.UpdateInterval);
    };
}]);