VisualizeApp.service('AppService', ['$http',
	function ($http){

		this.getMethodRealtime = function(id){
			return $http({
				method: "get",
				url: "http://127.0.0.1:8084/api/Realtime/getActivePower"
			});
		}

		this.getMethodAggregation = function(id){
			return $http({
				method: "get",
				url: "http://127.0.0.1:8084/api/Aggregation/AvgActivePower"
			});
		};
	}
]);
