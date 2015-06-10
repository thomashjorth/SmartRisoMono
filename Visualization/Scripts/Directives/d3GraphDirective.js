﻿VisualizeApp.directive('d3GraphDirective', function($parse, $window){
   return{
      restrict:'EA',
      template:"<svg></svg>",
       link: function(scope, elem, attrs){
            var exp = $parse(attrs.chartData);

            var dataToPlot=exp(scope);
            var padding = 40;
            var pathClass="path";
            var xScale, yScale, xAxisGen, yAxisGen, lineFun;

            var d3 = $window.d3;
            var rawSvg=elem.find('svg');
            var svg = d3.select(rawSvg[0]);
            var parentHeight = svg.node().parentNode.getBoundingClientRect().height;

            scope.$watchCollection(exp, function(newVal, oldVal){
                dataToPlot=newVal;
                redrawLineChart();
            });


            var margin = {top: (parentHeight*0.95)*0.04, right: (parentHeight*0.95)*0.16, bottom: (parentHeight*0.95)*0.06, left: (parentHeight*0.95)*0.1},
                width = (parentHeight*0.95)*1.92 - margin.left - margin.right,
                height = parentHeight*0.95 - margin.top - margin.bottom;

            svg
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom);

            var textPos=((parentHeight*0.95)*1.92-10);
           var parseDate = d3.time.format("%d-%b-%y").parse;
           function setChartParameters(){

               xScale = d3.time.scale()
                   .domain([dataToPlot.values[0].timestamp, dataToPlot.values[dataToPlot.values.length-1].timestamp])
              //     .domain(d3.extent(dataToPlot, function(d) { return d.timestamp; }))
                   .range([0, width]);

               var min = dataToPlot.config.yMin, max = dataToPlot.config.yMax;
               if(dataToPlot.config.yMin == null)
                  min = d3.min(dataToPlot.values, function (d) { return d.value;});
               if(dataToPlot.config.yMax == null)
                  max = d3.max(dataToPlot.values, function (d) { return d.value;});

               yScale = d3.scale.linear()
                   .domain([min, max])
                   .range([height-10, 10]);


               xAxisGen = d3.svg.axis()
                   .scale(xScale)
                   .orient("bottom")
                   .ticks(4)
                   .tickFormat(d3.time.format('%X'));

               yAxisGen = d3.svg.axis()
                   .scale(yScale)
                   .orient("left")
                   .ticks(5);

               lineFun = d3.svg.line()
                   .x(function (d) {
                       return xScale(d.timestamp);
                   })
                   .y(function (d) {
                       return yScale(d.value);
                   });
           }
         
         function drawLineChart() {

               setChartParameters();

               svg.append("svg:g")
                   .attr("class", "x axis")
                   .attr("transform", "translate(40," + Math.round(height-0) + ")")
                   .call(xAxisGen);

               svg.append("svg:g")
                   .attr("class", "y axis")
                   .attr("transform", "translate(40,0)")
                   .call(yAxisGen)
                    .append("text")
                        .attr("transform", "rotate(-90)")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .text(dataToPlot.config.unit);

               svg.selectAll(".tick").each(function (d, i) {
                      d3.select(this).style("font-size", (width*0.025));
                    });
               svg.append("svg:g")
                   .attr("class", "title")
                   .attr("transform", "translate("+textPos+",0)")
                   .append("text")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .text(dataToPlot.config.title);

               svg.append("svg:g")
                   .attr("class", "data")
                   .attr("transform", "translate("+textPos+","+height*0.1+")")
                   .append("text")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .style("font-size", (width*0.025))
                        .text("AVG: 0.0, MIN: 0.0, MAX 0.0");

               svg.append("svg:path")
                   .attr({
                       d: lineFun(dataToPlot.values),
                       "stroke": "blue",
                       "stroke-width": 2,
                       "fill": "none",
                       "class": pathClass
                   })
                   .attr("transform", "translate(40,0)");

           }

           function redrawLineChart() {

               setChartParameters();
               // Shows all data
               svg.selectAll("g.y.axis").call(yAxisGen);
               svg.selectAll("g.x.axis").call(xAxisGen);
               svg.selectAll("path.line").remove();
               svg.selectAll("."+pathClass)
                   .attr({
                       d: lineFun(dataToPlot.values)
                   });
               svg.select("g.x.axis")
                .selectAll("text")
                .style("font-size",(width*0.025)); //To change the font size of texts



               svg.selectAll(".data").remove();

               svg.append("svg:g")
                   .attr("class", "data")
                   .attr("transform", "translate("+textPos+","+height*0.1+")")
                   .append("text")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .style("font-size", (width*0.025))
                        .text("AVG: "+Math.round(dataToPlot.statistics.avg * 100) / 100+", MIN: "+dataToPlot.statistics.min+", MAX "+dataToPlot.statistics.max+"");
           }

           drawLineChart();
       }
   };
});

