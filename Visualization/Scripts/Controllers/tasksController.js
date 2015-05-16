VisualizeApp.controller('tasksController', ['$scope','$interval', '$http', 'AppService', function($scope, $interval, $http, AppService){
    

      $scope.params = $scope.init.Params + "&resource="

      $scope.startUnit = function () {
            AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.params+ "startLoad")
            .success(function (){
                  alert("success");
                  $scope.message = "Unit started."
            })
            .error(function (error) {
                  alert(error);
                  $scope.message = "Error: unit not started."     
            });
      }

      $scope.stopUnit = function () {
            AppService.putData($scope.init.Host,$scope.init.Port,$scope.init.Device,$scope.params+"stopLoad")
            .success(function (response){
                  $scope.message = "Unit stoped."
            });
      }

}]);
