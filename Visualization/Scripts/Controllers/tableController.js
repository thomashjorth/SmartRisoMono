VisualizeApp.controller('tableController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    $scope.initialize = function()
    {
   
         AppService.getData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.init.Method)
            .success(function (response){
            	$scope.Table = JSON.parse(response);
            });
          
    };
}]);
