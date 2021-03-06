VisualizeApp.directive('experimentDirective', function($parse, $window, $compile){
    return {
        restrict: 'AEC',
        template: "<div width='100%' height='100%'></div>",
        link: function (scope, elem, attrs) {
            var exp = $parse(attrs.chartData);

            var Data=exp(scope);

            var d3 = $window.d3;
            var rawdiv=elem.find('div');
            var div = d3.select(rawdiv[0]);
            var svg = div.append("svg").attr("width", "100%").attr("height", "100%");
            var width = div.node().getBoundingClientRect().width;
            var height = div.node().getBoundingClientRect().height;

            scope.$watchCollection(exp, function(newVal, oldVal){
                Data=newVal;
                if(oldVal == newVal)
                    draw(Data.config.units)
                else
                    updateValues(Data.values);
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
                    for(var i = 0; i < upper; i++){
                        //alert(scope.Units[i].Visualization1.VisualizationType)
                        //scope.init.push(scope.Units[i].Visualization1)
                        div.append("ng-include")
                            .attr("class", "exp upper1"+i)
                            .attr("z-index", "-1")
                            .attr("value", i*2)
                            .style("width", height/4.5*2)
                            .style("height", height/4.5)
                            .style("position", "absolute")
                            .style("top","25px")
                            .style("left",width/(upper)*(i)+125+"px")
                            .attr("src", data[i].Visualization1.VisualizationType);
                            //.attr("ng-init", "init.push(Units["+i+"].Visualization1)");//Units["+i+"].Visualization1");

                        div.append("ng-include")
                            .attr("class", "exp upper2"+i)
                            .attr("z-index", "-1")
                            .attr("value", i*2+1)
                            .style("width", height/4.5*2)
                            .style("height", height/4.5)
                            .style("position", "absolute")
                            .style("top",height/4.5+25)
                            .style("left",width/(upper)*(i)+125+"px")
                            .attr("src", data[i].Visualization2.VisualizationType);
                            //.attr("ng-init", "init.push(Units["+i+"].Visualization2)");//Units["+i+"].Visualization2");

                        svg.append("image")
                            .attr("xlink:href","http://"+data[i].Host+":"+data[i].Port+"/api/Image/Get/"+data[i].Device)
                            .attr("width","150px")
                            .attr("height","150px")
                            .attr("y", 50+"px")
                            .attr("x", width/(upper)*(i)+"px");

                        svg.append("line")
                            .attr("x1",width/(upper)*(i)+75)
                            .attr("y1",225)
                            .attr("x2",width/(upper)*(i)+75)
                            .attr("y2",height/2)
                            .style("stroke","rgb(0,0,0)")
                            .style("stroke-width",2);

                        svg.append("svg:g")
                            .attr("class", "databla")
                            .append("text")
                                .attr("x", width/(upper)*(i)+70)
                                .attr("y", 230)
                                .attr("dy", ".71em")
                                .style("text-anchor", "end")
                                .style("font-size", (width*0.01))
                                .text("0 " + data[i].Unit);

                    }
                    for(var i = 0; i < lower; i++){
                        div.append("ng-include")
                            .attr("class", "exp lower1"+i)
                            .attr("z-index", "-1")
                            .attr("value", (i+upper)*2)
                            .style("width", height/4.5*2)
                            .style("height", height/4.5)
                            .style("position", "absolute")
                            .style("bottom",height/4.5+25)
                            .style("left",width/(lower)*(i)+145+"px")
                            .attr("src", data[i+upper].Visualization1.VisualizationType);
                            //.attr("ng-init", "init.push(Units["+(i+upper)+"].Visualization1)");//Units["+i+"].Visualization1");

                        div.append("ng-include")
                            .attr("class", "exp lower2"+i)
                            .attr("z-index", "-1")
                            .attr("value", (i+upper)*2+1)
                            .style("width", height/4.5*2)
                            .style("height", height/4.5)
                            .style("position", "absolute")
                            .style("bottom","25px")
                            .style("left",width/(lower)*(i)+145+"px")
                            .attr("src", data[i+upper].Visualization2.VisualizationType);
                            //.attr("ng-init", "init.push(Units["+(i+upper)+"].Visualization2)");//Units["+i+"].Visualization2");

                        svg.append("image")
                            .attr("xlink:href","http://"+data[i+upper].Host+":"+data[i+upper].Port+"/api/Image/Get/"+data[i+upper].Device)
                            .attr("width","150px")
                            .attr("height","150px")
                            .attr("y", height-200+"px")
                            .attr("x", width/(lower)*(i)+"px");

                        svg.append("line")
                            .attr("x1",width/(lower)*(i)+75)
                            .attr("y1",height/2)
                            .attr("x2",width/(lower)*(i)+75)
                            .attr("y2",height-225)
                            .style("stroke","rgb(0,0,0)")
                            .style("stroke-width",2);

                        svg.append("svg:g")
                            .attr("class", "databla")
                            .append("text")
                                .attr("x", width/(lower)*(i)+70)
                                .attr("y", height-240)
                                .attr("dy", ".71em")
                                .style("text-anchor", "end")
                                .style("font-size", (width*0.01))
                                .text("0 "+data[i+upper].Unit);
                    }
                }
                svg.append("line")
                    .attr("x1",75)
                    .attr("y1",height/2)
                    .attr("x2",width/(upper)*(upper-1)+75)
                    .attr("y2",height/2)
                    .style("stroke","rgb(0,0,0)")
                    .style("stroke-width",2);
                $compile(elem.contents())(scope)
            }

            function drawlines(units) {

            }

            function addVisualization(device, visualization, data) {

            }

            function updateValues(data) {
                svg.selectAll("text").each(function (d, i) {
                    if(data[i].measurement != null){
                        if(data[i].measurement.value > 0){
                            d3.select(this)
                                .style("fill", "green")
                                .text(data[i].measurement.value+" "+Data.config.units[i].Unit)
                        } else{
                            d3.select(this)
                                .style("fill", "red")
                                .text(data[i].measurement.value+" "+Data.config.units[i].Unit)
                        }
                    }
                    });
                
            }
        }
    }
});