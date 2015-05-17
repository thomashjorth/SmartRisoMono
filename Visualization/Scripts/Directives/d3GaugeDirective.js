VisualizeApp.directive('d3GaugeDirective', function($parse, $window){
   return{
      restrict:'AEC',
      template:"<svg></svg>",
       link: function(scope, elem, attrs){
			var exp = $parse(attrs.chartData);

			var data=exp(scope);

			var d3 = $window.d3;
			var rawSvg=elem.find('svg');
			var svg = d3.select(rawSvg[0]);
			var _currentRotation;
			var self = this;
			var config = 
			{
				size: $('.box').outerHeight()*0.95,
				label: 'Power',
				min: -1,
				max: 1,
				minorTicks: 5
			}

			scope.$watchCollection(exp, function(newVal, oldVal){
				data=newVal;

				if(oldVal == newVal){
					config = 
					{
						size: $('.box').outerHeight()*0.95,
						label: data.config.label,
						min: data.config.min,
						max: data.config.max,
						minorTicks: 5,
						green: data.config.green,
						yellow: data.config.yellow,
						red: data.config.red
					}
					configure(config);
					render();
				}

				redraw();
			});

			function configure(configuration)
			{
				this.config = configuration;

				this.config.raduis = this.config.size*0.90 / 2;
				this.config.cx = this.config.size*0.97 / 2;
				this.config.cy = this.config.size*0.97 / 2;

				this.config.min = undefined != configuration.min ? configuration.min : 0; 
				this.config.max = undefined != configuration.max ? configuration.max : 100; 
				this.config.range = this.config.max - this.config.min;

				this.config.majorTicks = configuration.majorTicks || 5;
				this.config.minorTicks = configuration.minorTicks || 2;

				//this.config.green = [[0,100]];
				//this.config.yellow = [[100,150]];
				//this.config.red = [[150,300]];

				//this.config.range= this.config.max-this.config.min;
				//this.config.yellowZones = [{ from: config.min + range*0.75, to: config.min + range*0.9 }];
				//this.config.redZones = [{ from: config.min + range*0.9, to: config.min + range }];

				this.config.greenColor 	= configuration.greenColor || "#109618";
				this.config.yellowColor = configuration.yellowColor || "#FF9900";
				this.config.redColor 	= configuration.redColor || "#DC3912";

				this.config.transitionDuration = configuration.transitionDuration || 500;
			}
         
			function render()
			{

				this.body = svg
					.attr("class", data.config.PlaceHolder)
					.attr("width", config.size)
					.attr("height", config.size);

				this.body.append("svg:svg")
					.attr("class", "gauge")
					.attr("width", config.size)
					.attr("height", config.size);

				this.body.append("svg:circle")
					.attr("cx", config.cx)
					.attr("cy", config.cy)
					.attr("r", config.raduis)
					.style("fill", "#ccc")
					.style("stroke", "#000")
					.style("stroke-width", "0.5px");

				this.body.append("svg:circle")
					.attr("cx", config.cx)
					.attr("cy", config.cy)
					.attr("r", 0.9 * config.raduis)
					.style("fill", "#fff")
					.style("stroke", "#e0e0e0")
					.style("stroke-width", "2px");

				for (var index in config.green)
				{
					drawBand(config.green[index][0], config.green[index][1], self.config.greenColor);
				}

				for (var index in config.yellow)
				{
					drawBand(config.yellow[index][0], config.yellow[index][1], self.config.yellowColor);
				}

				for (var index in config.red)
				{
					drawBand(config.red[index][0], config.red[index][1], self.config.redColor);
				}

				if (undefined != config.label)
				{
					var fontSize = Math.round(config.size / 15);
					this.body.append("svg:text")
						.attr("x", config.cx)
						.attr("y", (config.cy / 2 + fontSize / 2)*2.75)
						.attr("dy", fontSize / 2)
						.attr("text-anchor", "middle")
						.text(config.label)
						.style("font-size", fontSize + "px")
						.style("fill", "#333")
						.style("stroke-width", "0px");
				}

				if (undefined != data.config.title)
				{
					var fontSize = Math.round(config.size / 15);
					this.body.append("svg:text")
						.attr("x", config.cx)
						.attr("y", (config.cy / 2 + fontSize / 2))
						.attr("dy", fontSize / 2)
						.attr("text-anchor", "middle")
						.text(data.config.title)
						.style("font-size", fontSize + "px")
						.style("fill", "#333")
						.style("stroke-width", "0px");
				}

				var fontSize = Math.round(config.size / 16);
				var majorDelta = config.range / (config.majorTicks - 1);
				for (var major = config.min; major <= config.max; major += majorDelta)
				{
					var minorDelta = majorDelta / config.minorTicks;
					for (var minor = major + minorDelta; minor < Math.min(major + majorDelta, this.config.max); minor += minorDelta)
					{
						var point1 = valueToPoint(minor, 0.75);
						var point2 = valueToPoint(minor, 0.85);

						this.body.append("svg:line")
							.attr("x1", point1.x)
							.attr("y1", point1.y)
							.attr("x2", point2.x)
							.attr("y2", point2.y)
							.style("stroke", "#666")
							.style("stroke-width", "1px");
					}

					var point1 = valueToPoint(major, 0.7);
					var point2 = valueToPoint(major, 0.85);	

					this.body.append("svg:line")
						.attr("x1", point1.x)
						.attr("y1", point1.y)
						.attr("x2", point2.x)
						.attr("y2", point2.y)
						.style("stroke", "#333")
						.style("stroke-width", "2px");

					if (major == config.min || major == config.max)
					{
						var point = valueToPoint(major, 0.63);

						this.body.append("svg:text")
							.attr("x", point.x)
							.attr("y", point.y)
							.attr("dy", fontSize / 3)
							.attr("text-anchor", major == config.min ? "start" : "end")
							.attr("text-alignment", "center")
							.text(major)
							.style("font-size", fontSize + "px")
							.style("fill", "#333")
							.style("stroke-width", "0px");
					}
					else if(major == majorDelta*2+config.min){
						var point = valueToPoint(major, 0.60);
						var didgits=(""+major).length;
						this.body.append("svg:text")
							.attr("x", point.x+7.5*(didgits/3))
							.attr("y", point.y)
							.attr("dy", fontSize / 3)
							.attr("text-anchor", major == config.min ? "start" : "end")
							.text(major)
							.style("font-size", fontSize + "px")
							.style("fill", "#333")
							.style("stroke-width", "0px");
					}
					else if(major == majorDelta+config.min){
						var point = valueToPoint(major, 0.63);
						var didgits=(""+major).replace(".","").length;
						this.body.append("svg:text")
							.attr("x", point.x+didgits*5)
							.attr("y", point.y)
							.attr("dy", fontSize / 3)
							.attr("text-anchor", major == config.min ? "start" : "end")
							.text(major)
							.style("font-size", fontSize + "px")
							.style("fill", "#333")
							.style("stroke-width", "0px");
					}
					else{
						var point = valueToPoint(major, 0.63);
						this.body.append("svg:text")
							.attr("x", point.x)
							.attr("y", point.y)
							.attr("dy", fontSize / 3)
							.attr("text-anchor", major == config.min ? "start" : "end")
							.text(major)
							.style("font-size", fontSize + "px")
							.style("fill", "#333")
							.style("stroke-width", "0px");
					}
				}

				var pointerContainer = this.body.append("g").attr("class", "pointerContainer").attr("id", data.config.PlaceHolder);

				var midValue = (config.min + config.max) / 2;

				var pointerPath = buildPointerPath(midValue);

				var pointerLine = d3.svg.line()
						.x(function(d) { return d.x })
						.y(function(d) { return d.y })
						.interpolate("basis");

				pointerContainer.selectAll("path")
					.data([pointerPath])
					.enter()
					.append("svg:path")
					.attr("d", pointerLine)
					.style("fill", "#dc3912")
					.style("stroke", "#c63310")
					.style("fill-opacity", 0.7)

				pointerContainer.append("svg:circle")
					.attr("cx", config.cx)
					.attr("cy", config.cy)
					.attr("r", 0.12 * config.raduis)
					.style("fill", "#4684EE")
					.style("stroke", "#666")
					.style("opacity", 1);

				var fontSize = Math.round(config.size / 15);
				pointerContainer.selectAll("text")
					.data([midValue])
					.enter()
					.append("svg:text")
					.attr("x", config.cx)
					.attr("y", (config.size - config.cy / 4 - fontSize)*0.75)
					.attr("dy", fontSize / 2)
					.attr("text-anchor", "middle")
					.style("font-size", fontSize + "px")
					.style("fill", "#000")
					.style("stroke-width", "0px");

				redraw(this.config.min, 0);
			}

			function buildPointerPath(value)
			{
				var delta = config.range / 13;

				var head = valueToPointInner(value, 0.85);
				var head1 = valueToPointInner(value - delta, 0.12);
				var head2 = valueToPointInner(value + delta, 0.12);

				var tailValue = value - (config.range * (1/(270/360)) / 2);
				var tail = valueToPointInner(tailValue, 0.28);
				var tail1 = valueToPointInner(tailValue - delta, 0.12);
				var tail2 = valueToPointInner(tailValue + delta, 0.12);

				return [head, head1, tail2, tail, tail1, head2, head];

				function valueToPointInner(value, factor)
				{
					var point = valueToPoint(value, factor);
					point.x -= config.cx;
					point.y -= config.cy;
					return point;
				}
			}

			function drawBand(start, end, color)
			{
				if (0 >= end - start) return;

				this.body.append("svg:path")
					.style("fill", color)
					.attr("d", d3.svg.arc()
					.startAngle(valueToRadians(start))
					.endAngle(valueToRadians(end))
					.innerRadius(0.70 * config.raduis)
					.outerRadius(0.85 * config.raduis))
					.attr("transform", function() { return "translate(" + config.cx + ", " + config.cy + ") rotate(270)" });
			}

			function redraw(transitionDuration)
			{
				//alert("#" + data.config.PlaceHolder + " " +data.CompositeMeasurement.v);
				var pointerContainer = d3.select("#"+data.config.PlaceHolder);

				pointerContainer.select("text").text(data.CompositeMeasurement.v);

				var pointer = pointerContainer.select("path");
				pointer.transition()
					.duration(undefined != transitionDuration ? transitionDuration : this.config.transitionDuration)
					//.delay(0)
					//.ease("linear")
					//.attr("transform", function(d) 
					.attrTween("transform", function()
					{
						var pointerValue = data.CompositeMeasurement.v;
						if (data.CompositeMeasurement.v > config.max) pointerValue = config.max + 0.02*config.range;
						else if (data.CompositeMeasurement.v < config.min) pointerValue = config.min - 0.02*config.range;
						var targetRotation = (valueToDegrees(pointerValue) - 90);
						var currentRotation = _currentRotation || targetRotation;
						_currentRotation = targetRotation;

						return function(step) 
						{
							var rotation = currentRotation + (targetRotation-currentRotation)*step;
							return "translate(" + config.cx + ", " + config.cy + ") rotate(" + rotation + ")"; 
						}
					});
			}

			function valueToDegrees(value)
			{
				// thanks @closealert
				//return value / this.config.range * 270 - 45;
				return value / config.range * 270 - (config.min / config.range * 270 + 45);
			}

			function valueToRadians(value)
			{
				return valueToDegrees(value) * Math.PI / 180;
			}

			function valueToPoint(value, factor)
			{
				return { 	x: config.cx - config.raduis * factor * Math.cos(valueToRadians(value)),
							y: config.cy - config.raduis * factor * Math.sin(valueToRadians(value)) 		
				};
			}
       }
   };
});
