VisualizeApp.controller('experimentController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService, experimentDirective){

    $scope.run = true;
	$scope.initialize = function(gauge)
  	{
        

        $scope.Params = $scope.gauge.Params;
        $interval(function(){

        }, 10000);
    };
}]);
