VisualizeApp.directive('d3GaugeDirective', function($parse, $window){
   return{
      restrict:'AEC',
      template:"<svg width='400' height='400'></svg>",
       link: function(scope, elem, attrs){
			var exp = $parse(attrs.chartData);

			var CompositeMeasurementToGauge=exp(scope);
			//var padding = 40;
			//var pathClass="path";
			//var xScale, yScale, xAxisGen, yAxisGen, lineFun;

			var d3 = $window.d3;
			var rawSvg=elem.find('svg');
			var svg = d3.select(rawSvg[0]);

			var self = this;

			var config = 
			{
				size: 200,
				label: 'Power',
				min: -1,
				max: 1,
				minorTicks: 5
			}

			scope.$watchCollection(exp, function(newVal, oldVal){
				CompositeMeasurementToGauge=newVal;
				redraw();
			});

			function configure(configuration)
			{
				this.config = configuration;

				this.config.size = this.config.size * 0.9;

				this.config.raduis = this.config.size * 0.97 / 2;
				this.config.cx = this.config.size / 2;
				this.config.cy = this.config.size / 2;

				this.config.min = undefined != configuration.min ? configuration.min : 0; 
				this.config.max = undefined != configuration.max ? configuration.max : 100; 
				this.config.range = this.config.max - this.config.min;

				this.config.majorTicks = configuration.majorTicks || 5;
				this.config.minorTicks = configuration.minorTicks || 2;

				this.config.greenColor 	= configuration.greenColor || "#109618";
				this.config.yellowColor = configuration.yellowColor || "#FF9900";
				this.config.redColor 	= configuration.redColor || "#DC3912";

				this.config.transitionDuration = configuration.transitionDuration || 500;
			}
         
			function render()
			{
				this.body = svg//d3.select("#" + this.placeholderName)
					.append("svg:svg")
					.attr("class", "gauge")
					.attr("width", this.config.size)
					.attr("height", this.config.size);

				this.body.append("svg:circle")
					.attr("cx", this.config.cx)
					.attr("cy", this.config.cy)
					.attr("r", this.config.raduis)
					.style("fill", "#ccc")
					.style("stroke", "#000")
					.style("stroke-width", "0.5px");

				this.body.append("svg:circle")
					.attr("cx", this.config.cx)
					.attr("cy", this.config.cy)
					.attr("r", 0.9 * this.config.raduis)
					.style("fill", "#fff")
					.style("stroke", "#e0e0e0")
					.style("stroke-width", "2px");

				for (var index in this.config.greenZones)
				{
					drawBand(this.config.greenZones[index].from, this.config.greenZones[index].to, self.config.greenColor);
				}

				for (var index in this.config.yellowZones)
				{
					drawBand(this.config.yellowZones[index].from, this.config.yellowZones[index].to, self.config.yellowColor);
				}

				for (var index in this.config.redZones)
				{
					drawBand(this.config.redZones[index].from, this.config.redZones[index].to, self.config.redColor);
				}

				if (undefined != this.config.label)
				{
					var fontSize = Math.round(this.config.size / 9);
					this.body.append("svg:text")
						.attr("x", this.config.cx)
						.attr("y", this.config.cy / 2 + fontSize / 2)
						.attr("dy", fontSize / 2)
						.attr("text-anchor", "middle")
						.text(this.config.label)
						.style("font-size", fontSize + "px")
						.style("fill", "#333")
						.style("stroke-width", "0px");
				}

				var fontSize = Math.round(this.config.size / 16);
				var majorDelta = this.config.range / (this.config.majorTicks - 1);
				for (var major = this.config.min; major <= this.config.max; major += majorDelta)
				{
					var minorDelta = majorDelta / this.config.minorTicks;
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

					if (major == this.config.min || major == this.config.max)
					{
						var point = valueToPoint(major, 0.63);

						this.body.append("svg:text")
							.attr("x", point.x)
							.attr("y", point.y)
							.attr("dy", fontSize / 3)
							.attr("text-anchor", major == this.config.min ? "start" : "end")
							.text(major)
							.style("font-size", fontSize + "px")
							.style("fill", "#333")
							.style("stroke-width", "0px");
					}
				}

				var pointerContainer = this.body.append("svg:g").attr("class", "pointerContainer");

				var midValue = (this.config.min + this.config.max) / 2;

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
					.attr("cx", this.config.cx)
					.attr("cy", this.config.cy)
					.attr("r", 0.12 * this.config.raduis)
					.style("fill", "#4684EE")
					.style("stroke", "#666")
					.style("opacity", 1);

				var fontSize = Math.round(this.config.size / 10);
				pointerContainer.selectAll("text")
					.data([midValue])
					.enter()
					.append("svg:text")
					.attr("x", this.config.cx)
					.attr("y", this.config.size - this.config.cy / 4 - fontSize)
					.attr("dy", fontSize / 2)
					.attr("text-anchor", "middle")
					.style("font-size", fontSize + "px")
					.style("fill", "#000")
					.style("stroke-width", "0px");

				redraw(this.config.min, 0);
			}

			function buildPointerPath(value)
			{
				var delta = this.config.range / 13;

				var head = valueToPointInner(value, 0.85);
				var head1 = valueToPointInner(value - delta, 0.12);
				var head2 = valueToPointInner(value + delta, 0.12);

				var tailValue = value - (this.config.range * (1/(270/360)) / 2);
				var tail = valueToPointInner(tailValue, 0.28);
				var tail1 = valueToPointInner(tailValue - delta, 0.12);
				var tail2 = valueToPointInner(tailValue + delta, 0.12);

				return [head, head1, tail2, tail, tail1, head2, head];

				function valueToPointInner(value, factor)
				{
					var point = valueToPoint(value, factor);
					point.x -= this.config.cx;
					point.y -= this.config.cy;
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
					.innerRadius(0.65 * this.config.raduis)
					.outerRadius(0.85 * this.config.raduis))
					.attr("transform", function() { return "translate(" + self.config.cx + ", " + self.config.cy + ") rotate(270)" });
			}

			function redraw(transitionDuration)
			{
				var pointerContainer = this.body.select(".pointerContainer");

				pointerContainer.selectAll("text").text(CompositeMeasurementToGauge.v);

				var pointer = pointerContainer.selectAll("path");
				pointer.transition()
					.duration(undefined != transitionDuration ? transitionDuration : this.config.transitionDuration)
					//.delay(0)
					//.ease("linear")
					//.attr("transform", function(d) 
					.attrTween("transform", function()
					{
						//alert(CompositeMeasurementToGauge.v);
						var pointerValue = CompositeMeasurementToGauge.v;
						if (CompositeMeasurementToGauge.v > self.config.max) pointerValue = self.config.max + 0.02*self.config.range;
						else if (CompositeMeasurementToGauge.v < self.config.min) pointerValue = self.config.min - 0.02*self.config.range;
						var targetRotation = (valueToDegrees(pointerValue) - 90);
						var currentRotation = self._currentRotation || targetRotation;
						self._currentRotation = targetRotation;

						return function(step) 
						{
							var rotation = currentRotation + (targetRotation-currentRotation)*step;
							return "translate(" + self.config.cx + ", " + self.config.cy + ") rotate(" + rotation + ")"; 
						}
					});
			}

			function valueToDegrees(value)
			{
				// thanks @closealert
				//return value / this.config.range * 270 - 45;
				return value / this.config.range * 270 - (this.config.min / this.config.range * 270 + 45);
			}

			function valueToRadians(value)
			{
				return valueToDegrees(value) * Math.PI / 180;
			}

			function valueToPoint(value, factor)
			{
				return { 	x: this.config.cx - this.config.raduis * factor * Math.cos(valueToRadians(value)),
							y: this.config.cy - this.config.raduis * factor * Math.sin(valueToRadians(value)) 		
				};
			}

			configure(config);
			render();
       }
   };
});

