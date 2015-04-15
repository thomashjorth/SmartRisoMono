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

            var margin = {top: ($('.box').outerHeight()*0.95)*0.06, right: ($('.box').outerHeight()*0.95)*0.02, bottom: ($('.box').outerHeight()*0.95)*0.02, left: ($('.box').outerHeight()*0.95)*0.16},
                width = ($('.box').outerHeight()*0.95)*1.92 - margin.left - margin.right,
                height = $('.box').outerHeight()*0.95 - margin.top - margin.bottom;


            svg 
                .attr("width", width + margin.left + margin.right)
                .attr("height", height + margin.top + margin.bottom)
            var g = svg.append("g")
                .attr("transform", "translate(" + margin.left + "," + margin.top + ")");
            g.append("g")
                .attr("class", "xaxisBar x axis");
            g.append("g")
                .attr("class", "yaxisBar y axis");
            scope.$watchCollection(exp, function(newVal, oldVal){
                BarChart=newVal;

                var data = [];
                for (var i = 0; i < newVal.length; i++) {
                    data.push(BarChart[i].value)
                };

                if(newVal == oldVal){
                    draw(data)
                }else {
                    redraw(data)
                }
            });

            function draw (data) {
                var y0 = Math.max(Math.abs(d3.min(data)), Math.abs(d3.max(data)));

                var y = d3.scale.linear()
                    .domain([-y0, y0])
                    .range([height,0])
                    .nice();

                var x = d3.scale.ordinal()
                    .domain(d3.range(data.length))
                    .rangeRoundBands([0, width], .2);

                var yAxis = d3.svg.axis()
                    .scale(y)
                    .orient("left");

                g.selectAll(".bar")
                    .data(data)
                  .enter().append("rect")
                    .attr("class", function(d) { return d < 0 ? "bar negative" : "bar positive"; })
                    .attr("y", function(d) { return y(Math.max(0, d)); })
                    .attr("x", function(d, i) { return x(i); })
                    .attr("height", function(d) { return Math.abs(y(d) - y(0)); })
                    .attr("width", x.rangeBand());

                g.append("g")
                    .attr("class", "xaxisBar x axis")
                    .call(yAxis);

                g.append("g")
                    .attr("class", "yaxisBar y axis")
                    .append("line")
                    .attr("y1", y(0))
                    .attr("y2", y(0))
                    .attr("x1", 0)
                    .attr("x2", width);
            }

            function redraw (data) {
                var y0 = Math.max(Math.abs(d3.min(data)), Math.abs(d3.max(data)));

                var y = d3.scale.linear()
                    .domain([-y0, y0])
                    .range([height,0])
                    .nice();

                var x = d3.scale.ordinal()
                    .domain(d3.range(data.length))
                    .rangeRoundBands([0, width], .2);

                var yAxis = d3.svg.axis()
                    .scale(y)
                    .orient("left");

                g.selectAll("rect")
                    .data(data)
                    .transition().duration(500)
                    .attr("class", function(d) { return d < 0 ? "bar negative" : "bar positive"; })
                    .attr("y", function(d) { return y(Math.max(0, d)); })
                    .attr("x", function(d, i) { return x(i); })
                    .attr("height", function(d) { return Math.abs(y(d) - y(0)); });

                $(".xaxisBar")
                    .remove();

                $(".yaxisBar")
                    .remove();

                g.append("g")
                    .attr("class", "xaxisBar x axis");
                g.append("g")
                    .attr("class", "yaxisBar y axis");

                g.append("g")
                    .attr("class", "xaxisBar x axis")
                    .call(yAxis);

                g.append("g")
                    .attr("class", "yaxisBar y axis")
                    .append("line")
                    .attr("y1", y(0))
                    .attr("y2", y(0))
                    .attr("x1", 0)
                    .attr("x2", width);
            }
        }
    }    
});

