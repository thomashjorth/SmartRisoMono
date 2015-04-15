VisualizeApp.directive('linearChart', function($parse, $window){
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
                dataToPlot=newVal;
                if(newVal == oldVal)
                    draw(dataToPlot);
                else
                    redraw(dataToPlot);
            });

            var margin = {top: ($('.box').outerHeight()*0.95)*0.04, right: ($('.box').outerHeight()*0.95)*0.16, bottom: ($('.box').outerHeight()*0.95)*0.06, left: ($('.box').outerHeight()*0.95)*0.1},
                width = ($('.box').outerHeight()*0.95)*1.92 - margin.left - margin.right,
                height = $('.box').outerHeight()*0.95 - margin.top - margin.bottom;

            var parseDate = d3.time.format("%Y%m%d").parse;

            var x = d3.time.scale()
                .range([0, width]);

            var y = d3.scale.linear()
                .range([height, 0]);

            var color = d3.scale.category10();

            var xAxis = d3.svg.axis()
                .scale(x)
                .orient("bottom");

            var yAxis = d3.svg.axis()
                .scale(y)
                .orient("left");

            
            var line = d3.svg.line()
                   .x(function (d) {
                       return xScale(d.timestamp);
                   })
                   .y(function (d) {
                       return yScale(d.value);
                   });

            svg
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
            var g = svg.append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");



            function draw(data) {
                xScale = d3.scale.linear()
                   .domain([data[0].timestamp, data[data.length-1].timestamp])
                   .range([0, rawSvg.attr("width")]);

                yScale = d3.scale.linear()
                   .domain([d3.min(data, function (d) { return d.value;}), d3.max(data, function (d) { return d.value;})])
                   .range([height, 0]);
                
                xAxis = d3.svg.axis()
                    .scale(xScale)
                    .orient("bottom")
                    .ticks(10);

                yAxis = d3.svg.axis()
                    .scale(yScale)
                    .orient("left")
                    .ticks(5);

                g.append("g")
                    .attr("class", "x axis")
                    .attr("transform", "translate(0," + height + ")")
                    .call(xAxis);

                g.append("g")
                    .attr("class", "y axis")
                    .call(yAxis)
                    .append("text")
                        .attr("transform", "rotate(-90)")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .text("Price ($)");

                g.append("path")
                    .attr({
                        d: line(data),
                        "stroke": "blue",
                        "stroke-width": 2,
                        "fill": "none",
                        "class": "path"
                    })
                   .attr("transform", "translate(-10,0)");
            };

            function redraw(data) {
                xScale = d3.scale.linear()
                   .domain([data[0].timestamp, data[data.length-1].timestamp])
                   .range([10, rawSvg.attr("width")]);

               yScale = d3.scale.linear()
                   .domain([-1, 1])
                   .range([height, 0]);


                xAxis = d3.svg.axis()
                    .scale(xScale)
                    .orient("bottom")
                    .ticks(10);

                yAxis = d3.svg.axis()
                    .scale(yScale)
                    .orient("left")
                    .ticks(5);

                g.selectAll("y").call(yAxis);
                g.selectAll("x").call(xAxis);
                g.selectAll("line").remove();
                g.selectAll(".path")
                    .attr({
                       d: line(data)
                    });
            };
       }
   };
});

