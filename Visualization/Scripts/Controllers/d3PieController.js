VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.initialize = function()
    {
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Method)
            .success(function (response){
                if(JSON.parse(response).length == 1 && JSON.parse(response)[0].value == 0){
                    
                }else{
                    $scope.data = {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };
                }

            });

        $interval(function(){
            var h=Math.floor(Date.now()/1000)-$scope.firstTime;
            AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Method)
                .success(function (response){

                if(JSON.parse(response).length == 1 && JSON.parse(response)[0].value == 0){
                    
                }else{
                    $scope.data = {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };
                }

            });
        }, $scope.init.UpdateInterval);
    };
}]);