VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.initialize = function()
    {

        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.init.Resource)
            .success(function (response){

                $scope.data =
                {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };

            });
    };



    $interval(function(){
        var h=Math.floor(Date.now()/1000)-$scope.firstTime;
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Aggregation,$scope.init.Resource)
            .success(function (response){

                $scope.data =
                {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };

            });
    }, 10000);
}]);