VisualizeApp.directive('d3MultiGraphDirective', function($parse, $window){
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


			scope.$watchCollection(exp, function(newVal, oldVal){
				config = newVal.config;
				if(newVal == undefined){

				}else if(oldVal == undefined){
				dataToPlot=d3.nest()
				    .key(function(d) {
				        return d.name;
				    })
				    .entries(newVal.values);
					drawLineChart();
				}
				else{
				dataToPlot=d3.nest()
				    .key(function(d) {
				        return d.name;
				    })
				    .entries(newVal.values);
					redrawLineChart();
				}
			});


			var margin = {top: ($('.box').outerHeight()*0.95)*0.04, right: ($('.box').outerHeight()*0.95)*0.16, bottom: ($('.box').outerHeight()*0.95)*0.06, left: ($('.box').outerHeight()*0.95)*0.1},
				width = ($('.box').outerHeight()*0.95)*1.92 - margin.left - margin.right,
				height = $('.box').outerHeight()*0.95 - margin.top - margin.bottom;

			svg
				.attr("width", width + margin.left + margin.right)
				.attr("height", height + margin.top + margin.bottom);

		   var parseDate = d3.time.format("%d-%b-%y").parse;
		   function setChartParameters(){
		   	//alert(dataToPlot)
			   xScale = d3.time.scale()
				   .domain([dataToPlot[0].values[0].timestamp, dataToPlot[0].values[dataToPlot[0].values.length-1].timestamp])
			  //     .domain(d3.extent(dataToPlot, function(d) { return d.timestamp; }))
				   .range([0, $('.box').outerWidth()-(margin.right+margin.left)]);

			   var min = config.yMin, max = config.yMax;
			   /*if(dataToPlot.config.yMin == null)
				  min = d3.min(dataToPlot.values, function (d) { return d.value;});
			   if(dataToPlot.config.yMax == null)
				  max = d3.max(dataToPlot.values, function (d) { return d.value;});
*/
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
				   .attr("transform", "translate(30," + Math.round(height) + ")")
				   .call(xAxisGen);

			   svg.append("svg:g")
				   .attr("class", "y axis")
				   .attr("transform", "translate(30,0)")
				   .call(yAxisGen)
					.append("text")
						.attr("transform", "rotate(-90)")
						.attr("y", 6)
						.attr("dy", ".71em")
						.style("text-anchor", "end")
						.text(config.unit);

			   svg.append("svg:g")
				   .attr("class", "title")
				   .attr("transform", "translate("+(($('.box').outerHeight()*0.95)*1.92-10)+",0)")
				   .append("text")
						.attr("y", 6)
						.attr("dy", ".71em")
						.style("text-anchor", "end")
						.text(config.title);

				dataToPlot.forEach(function(d, i) {
					var color = "hsl(" + Math.random() * 360 + ",100%,50%)";

					svg.append('svg:path')
						.attr({
							d: lineFun(d.values),
							"stroke": color,
							'stroke-width': 2,
							"fill": 'none',
					   		'class': pathClass+i})
						.attr("transform", "translate(30,0)");

					svg.append("svg:g")
	                   	.attr("class", "data"+i)
	                   	.attr("transform", "translate("+((($('.box').outerHeight()*0.5)-10)+(i*25))+","+height*0+")")
	                   	.append("text")
	                        .attr("y", 6)
	                        .attr("dy", ".71em")
	                        .style("text-anchor", "end")
	                        .style("font-size", (width*0.035))
	                        .style("fill", color)
	                        .text(dataToPlot[i].key);
				});

		   }

		   function redrawLineChart() {
		   		//alert()
			   setChartParameters();
			   // Shows all data
			   svg.selectAll("g.y.axis").call(yAxisGen);
			   svg.selectAll("g.x.axis")
				   .call(xAxisGen);
			   //svg.selectAll("path.line").remove();
			   dataToPlot.forEach(function(d, i) {
			   	//alert()
			   		svg.selectAll("."+pathClass+i)
				   		.attr({
					   		d: lineFun(d.values)
				   		});
				});

		   }
	   }
   };
});

