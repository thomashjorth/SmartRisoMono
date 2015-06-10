VisualizeApp.directive('d3BarDirective', function($parse, $window){
   return {
       restrict: 'AEC',
       template: "<svg></svg>",
       link: function (scope, elem, attrs) {
            var exp = $parse(attrs.chartData);

            var BarChart=exp(scope);

            var d3 = $window.d3;
            var rawSvg=elem.find('svg');
            var svg = d3.select(rawSvg[0]);
            var x, y;

            var parentHeight = svg.node().parentNode.getBoundingClientRect().height;
            if(parentHeight == 0)
              parentHeight = $('.boxAll').outerHeight()*0.3*0.95;
            var margin = {top: (parentHeight)*0.06, right: (parentHeight)*0.02, bottom: (parentHeight)*0.02, left: (parentHeight)*0.16},
                width = (parentHeight)*1.92 - margin.left - margin.right,
                height = parentHeight - margin.top - margin.bottom,
                xValue = function(d) { return d[0]; },
                yValue = function(d) { return d[1]; },
                xScale = d3.scale.ordinal(),
                yScale = d3.scale.linear(),
                yAxis = d3.svg.axis().scale(yScale).orient("left"),
                xAxis = d3.svg.axis().scale(xScale);


            svg 
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
            var g = svg.append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
            g.append("g")
                .attr("class", "bars");
            g.append("g")
                .attr("class", "y axis");
            g.append("g")
                .attr("class", "x axis");
            g.append("g")
                .attr("class", "x axis zero");

            scope.$watchCollection(exp, function(newVal, oldVal){
                BarChart=newVal;

                var data = [];
                if(newVal != undefined){
                    for (var i = 0; i < newVal.LabeledInstance.length; i++) {
                        data.push([BarChart.LabeledInstance[i].label, BarChart.LabeledInstance[i].measurement.value])
                    };

                    data = data.map(function(d, i) {
                        return [xValue.call(data, d, i), yValue.call(data, d, i)];
                    });

                    if(oldVal == null || BarChart.LabeledInstance.length != oldVal.LabeledInstance.length){
                        draw(data)
                    }else {
                        redraw(data)
                    }
                }
            });

            function X(d) {
                return xScale(d[0]);
            }

            function Y0() {
                return yScale(0);
            }

            function Y(d) {
                return yScale(d[1]);
            }

            function draw (data) {
                var yMin = BarChart.config.min, yMax = BarChart.config.max;
                if(BarChart.config.min == null)
                    yMin = -d3.max(data.map(function(d) { return Math.abs(d[1]);}));
                if(BarChart.config.max == null)
                    yMax = d3.max(data.map(function(d) { return Math.abs(d[1]);}));



                xScale
                    .domain(data.map(function(d) { return d[0];} ))
                    .rangeRoundBands([0, width - margin.left - margin.right], 0.2);
                yScale
                    .domain([yMin,yMax])
                    .range([height - margin.top - margin.bottom, 0])
                    .nice();

                var bar = g.select(".bars").selectAll(".bar").data(data);
                bar.enter().append("rect");
                bar.exit().remove();
                bar.attr("class", function(d, i) { return d[1] < 0 ? "bar negative" : "bar positive"; })
                    .attr("x", function(d) { return X(d); })
                    .attr("y", function(d, i) { return d[1] < 0 ? Y0() : Y(d); })
                    .attr("width", xScale.rangeBand())
                    .attr("height", function(d, i) { return Math.abs( Y(d) - Y0() ); });

                g.select(".x.axis")
                    .attr("transform", "translate(0," + (height - margin.top - margin.bottom) + ")")
                    .call(xAxis.orient("bottom"));

                g.select(".x.axis.zero")
                    .attr("transform", "translate(0," + Y0() + ")")
                    .call(xAxis.tickFormat("").tickSize(0));

                g.select(".y.axis")
                    .call(yAxis);

                g.append("g")
                   .attr("class", "title")
                   .attr("transform", "translate("+(width*0.85)+",-10)")
                   .append("text")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .text(BarChart.config.title);
                
            }

            function redraw (data) {
                var yMin = BarChart.config.min, yMax = BarChart.config.max;
                if(BarChart.config.min == null)
                    yMin = -d3.max(data.map(function(d) { return Math.abs(d[1]);}));
                if(BarChart.config.max == null)
                    yMax = d3.max(data.map(function(d) { return Math.abs(d[1]);}));

                xScale
                    .domain(data.map(function(d) { return d[0];} ))
                    .rangeRoundBands([0, width - margin.left - margin.right], 0.2);

                yScale
                    .domain([yMin,yMax])
                    .range([height - margin.top - margin.bottom, 0])
                    .nice();

                var bar = g.selectAll("rect").data(data)
                .transition().duration(500)
                .attr("class", function(d, i) { return d[1] < 0 ? "bar negative" : "bar positive"; })
                    .attr("x", function(d) { return X(d); })
                    .attr("y", function(d, i) { return d[1] < 0 ? Y0() : Y(d); })
                    .attr("width", xScale.rangeBand())
                    .attr("height", function(d, i) { return Math.abs( Y(d) - Y0() ); });

                g.select(".x.axis.zero")
                    .attr("transform", "translate(0," + Y0() + ")")
                    .call(xAxis.tickFormat("").tickSize(0));

                g.select(".y.axis")
                    .call(yAxis);
            }
        }
    }    
});

