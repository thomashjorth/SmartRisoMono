VisualizeApp.directive('experimentDirective', function($parse, $window){
    return {
        restrict: 'AEC',
        template: "<svg width='100%' height='100%'></svg>",
        link: function (scope, elem, attrs) {
            var exp = $parse(attrs.chartData);

            var Data=exp(scope);

            var d3 = $window.d3;
            var rawsvg=elem.find('svg');
            var svg = d3.select(rawsvg[0]);
            var width = svg.node().getBoundingClientRect().width;
            var height = svg.node().getBoundingClientRect().height;

            scope.$watchCollection(exp, function(newVal, oldVal){
                Data=newVal;
                if(oldVal == newVal)
                    draw(Data.config.units)
            });

            function draw(data) {
                var upper = Math.round(data.length/2);
                var lower = data.length-upper;
                if(data.length <= 2){
                    for(var i = 0; i < data.length; i++){
                        svg.append("image")
                            .attr("xlink:href","http://"+data[i].Host+":"+data[i].Port+"/api/Image/Get/"+data[i].Device)
                            //.attr("width","10%")
                            .style("position","absolute")
                            .style("left", width/(data.length)*(i)+"px");
                    }
                } else {
                    alert(lower)
                    for(var i = 0; i < upper; i++){
                        svg.append("image")
                            .attr("xlink:href","http://"+data[i].Host+":"+data[i].Port+"/api/Image/Get/"+data[i].Device)
                            .attr("width","150px")
                            .attr("height","150px")
                            .style("y", 50+"px")
                            .style("x", width/(upper)*(i)+(width/(upper))/3+"px");

                        svg.append("line")
                            .attr("x1",width/(upper)*(i)+(width/(upper))/3+75)
                            .attr("y1",225)
                            .attr("x2",width/(upper)*(i)+(width/(upper))/3+75)
                            .attr("y2",height/2)
                            .style("stroke","rgb(0,0,0)")
                            .style("stroke-width",2);
                    }
                    for(var i = 0; i < lower; i++){
                        svg.append("image")
                            .attr("xlink:href","http://"+data[i].Host+":"+data[i].Port+"/api/Image/Get/"+data[i].Device)
                            .attr("width","150px")
                            .attr("height","150px")
                            .style("y", height-200+"px")
                            .style("x", width/(lower)*(i)+(width/(lower))/3+"px");

                        svg.append("line")
                            .attr("x1",width/(lower)*(i)+(width/(lower))/3+75)
                            .attr("y1",height/2)
                            .attr("x2",width/(lower)*(i)+(width/(lower))/3+75)
                            .attr("y2",height-225)
                            .style("stroke","rgb(0,0,0)")
                            .style("stroke-width",2);
                    }
                }
                svg.append("line")
                    .attr("x1",75+(width/(upper))/3)
                    .attr("y1",height/2)
                    .attr("x2",width/(upper)*(upper-1)+(width/(upper))/3+75)
                    .attr("y2",height/2)
                    .style("stroke","rgb(0,0,0)")
                    .style("stroke-width",2);
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