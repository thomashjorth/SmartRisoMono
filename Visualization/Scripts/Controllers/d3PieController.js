VisualizeApp.controller('d3PieController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, d3PieDirective){
    $scope.run=true;
    $scope.initialize = function()
    {
        $scope.temp = $scope.init;
        if($scope.init.length != undefined){
            $scope.temp=$scope.init.shift();
        }
        AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
            .success(function (response){
                //if(JSON.parse(response).length == 1 && JSON.parse(response)[0].value == 0){
                //}else{
                    $scope.data = {config: {label: $scope.temp.TitleHeading}, LabeledInstance: JSON.parse(response) };
                //}

            });

        $interval(function(){
            if($scope.run){
                $scope.run=false;
                AppService.getData($scope.temp.Host,$scope.temp.Port,$scope.temp.Device,$scope.temp.Params)
                    .success(function (response){

/*                    if(JSON.parse(response).length == 1 && JSON.parse(response)[0].measurement.value == 0){
                        
                    }else{
                        $scope.data = {config: {label: $scope.temp.TitleHeading}, LabeledInstance: JSON.parse(response) };
                    }
*/                  
                        $scope.data = {config: {label: $scope.temp.TitleHeading}, LabeledInstance: JSON.parse(response) };
                    $scope.run=true;
                    })
                    .error( function (response){
                        $scope.run=true;
                    });
            }
        }, $scope.temp.UpdateInterval);
    };
}]);