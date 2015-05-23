﻿VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.run=true;
    $scope.initialize = function()
    {
        AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
            .success(function (response){
                if(JSON.parse(response).length == 1 && JSON.parse(response)[0].value == 0){
                    
                }else{
                    $scope.data = {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };
                }

            });

        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Params)
                    .success(function (response){

                    if(JSON.parse(response).length == 1 && JSON.parse(response)[0].value == 0){
                        
                    }else{
                        $scope.data = {config: {label: $scope.init.TitleHeading}, LabeledInstance: JSON.parse(response) };
                    }
                    $scope.run=true;
                    })
                    .error( function (response){
                        $scope.run=true;
                    });
            }
        }, $scope.init.UpdateInterval);
    };
}]);