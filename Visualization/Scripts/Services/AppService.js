﻿VisualizeApp.service('AppService', ['$http',
	function ($http){

		this.getData = function(host, port, apiController, id){
			return $http({
				method: "get",
				url: "http://"+host+":"+port+"/api/"+apiController+"/"+id
			});
		}
	}
]);
