VisualizeApp.directive('linearChart', function($parse, $window){
   return{
      restrict:'EA',
      template:"<svg width='400' height='200'></svg>",
       link: function(scope, elem, attrs){
           var exp = $parse(attrs.chartData);

           var salesDataToPlot=exp(scope);
           var padding = 40;
           var pathClass="path";
           var xScale, yScale, xAxisGen, yAxisGen, lineFun;

           var d3 = $window.d3;
           var rawSvg=elem.find('svg');
           var svg = d3.select(rawSvg[0]);

           scope.$watchCollection(exp, function(newVal, oldVal){
               salesDataToPlot=newVal;
               redrawLineChart();
           });

           function setChartParameters(){

               xScale = d3.scale.linear()
                   .domain([salesDataToPlot[0].hour, salesDataToPlot[salesDataToPlot.length-1].hour])
                   .range([padding+10, rawSvg.attr("width")]);

               yScale = d3.scale.linear()
                   .domain([-1, 1])
                   .range([rawSvg.attr("height") - padding, 20]);

               xAxisGen = d3.svg.axis()
                   .scale(xScale)
                   .orient("bottom")
                   .ticks(10);

               yAxisGen = d3.svg.axis()
                   .scale(yScale)
                   .orient("left")
                   .ticks(5);

               lineFun = d3.svg.line()
                   .x(function (d) {
                       return xScale(d.hour);
                   })
                   .y(function (d) {
                       return yScale(d.sales);
                   });
           }
         
         function drawLineChart() {

               setChartParameters();

               svg.append("svg:g")
                   .attr("class", "x axis")
                   .attr("transform", "translate(-10,90)")
                   .call(xAxisGen);

               svg.append("svg:g")
                   .attr("class", "y axis")
                   .attr("transform", "translate(40,0)")
                   .call(yAxisGen);

               svg.append("svg:path")
                   .attr({
                       d: lineFun(salesDataToPlot),
                       "stroke": "blue",
                       "stroke-width": 2,
                       "fill": "none",
                       "class": pathClass
                   })
                   .attr("transform", "translate(-10,0)");
           }

           function redrawLineChart() {

               setChartParameters();
               // Shows all data
               svg.selectAll("g.y.axis").call(yAxisGen);
               svg.selectAll("g.x.axis").call(xAxisGen);
               svg.selectAll("path.line").remove();
               svg.selectAll("."+pathClass)
                   .attr({
                       d: lineFun(salesDataToPlot)
                   });
               svg.selectAll(".tick").each(function (d, i) {
        			if ( i == 0 ) {
            		this.remove();
        			}});
           }

           drawLineChart();
       }
   };
});

