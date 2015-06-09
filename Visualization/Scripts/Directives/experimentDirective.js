VisualizeApp.directive('experimentDirective', function($parse, $window){
    return {
        restrict: 'AEC',
        template: "<div></div>",
        link: function (scope, elem, attrs) {
            var exp = $parse(attrs.chartData);

            var Data=exp(scope);

            var d3 = $window.d3;
            var rawdiv=elem.find('div');
            var div = d3.select(rawDiv[0]);


            scope.$watchCollection(exp, function(newVal, oldVal){
                DataChart=newVal;


            });

            function draw(data) {
                
            }

            function drawlines(units) {

            }

            function addVisualization(device, visualization, data) {

            }

            function updateValues(data) {

            }
        }
    }
});