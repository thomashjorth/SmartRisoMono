VisualizeApp.service('AppService', ['$http',
	function ($http){

		this.getData = function(host, port, apiController, params){
			return $http({
				method: "get",
				url: "http://"+host+":"+port+"/api/"+apiController+"/"+params
			});
		}
		this.putData = function(host, port, apiController, params){
			var temp = $http({
				method: "put",
				url: "http://"+host+":"+port+"/api/"+apiController+"/"+params
			});
			return temp;
		}

	}
]);
