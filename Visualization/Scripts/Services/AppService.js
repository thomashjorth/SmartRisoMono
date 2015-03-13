VisualizeApp.service('AppService', ['$http',
	function ($http){
		//var headers = {
		//		'Access-Control-Allow-Origin' : '*',
		//		'Access-Control-Allow-Methods' : 'POST, GET, OPTIONS, PUT',
		//		'Content-Type': 'application/json',
		//		'Accept': 'application/json'
		//	};

		this.getMethod = function(id){
			return $http({
				method: "get",
				//headers: headers,
				url: "http://127.0.0.1:8084/api/Realtime/getActivePower"
			});
		};
	}
]);
