VisualizeApp.controller('d3BarController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3BarDirective){
    $scope.initialize = function()
    {

        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Method)
            .success(function (response){

                $scope.data = {config: {label: $scope.init.Unit, min: $scope.init.ValueMin, max: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                    LabeledInstance: JSON.parse(response) };

            });
        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Method)
                .success(function (response){
                $scope.data = {config: {label: $scope.init.Unit, min: $scope.init.ValueMin, max: $scope.init.ValueMax, title: $scope.init.TitleHeading}, 
                    LabeledInstance: JSON.parse(response) };
            });
        }, $scope.init.UpdateInterval);
    };
}]);