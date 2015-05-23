VisualizeApp.controller('d3BarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3BarDirective){
    $scope.run = true;
    $scope.initialize = function()
    {

        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
            .success(function (response){

                $scope.data = {config: {label: $scope.init.Unit, min: $scope.init.ValueMin, max: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                    LabeledInstance: JSON.parse(response) };

            });
        $interval(function(){
            if($scope.run){
                $scope.run = false;
                AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
                    .success(function (response){
                    $scope.data = {config: {label: $scope.init.Unit, min: $scope.init.ValueMin, max: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                        LabeledInstance: JSON.parse(response) };
                    $scope.run = true;
                })
                .error( function (response){
                    $scope.run=true;
                });
            }
        }, $scope.init.UpdateInterval);
    };
}]);