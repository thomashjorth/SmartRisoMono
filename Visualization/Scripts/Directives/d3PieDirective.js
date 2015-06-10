VisualizeApp.directive('d3PieDirective', function($parse, $window){
   return {
       restrict: 'AEC',
       template: "<svg></svg>",
       link: function (scope, elem, attrs) {
            var exp = $parse(attrs.chartData);

            var PieChart=exp(scope);

            var d3 = $window.d3;
            var rawSvg=elem.find('svg');
            var svg = d3.select(rawSvg[0]);

            var parentHeight = svg.node().parentNode.getBoundingClientRect().height;
            if(parentHeight == 0)
              parentHeight = $('.boxAll').outerHeight()*0.3*0.95;
            var color;

            scope.$watchCollection(exp, function(newVal, oldVal){
                PieChart=newVal;

                var list = [];
                if(PieChart!=undefined){
                    for (var i = 0; i < newVal.LabeledInstance.length; i++) {
                        list.push(PieChart.LabeledInstance[i].label)
                    };
                    color = d3.scale.category20()
                        .domain(list);

                    change(color.domain().map(function(label){
                            return { label: label }
                        }).sort(function(a,b) {
                            return d3.ascending(a.label, b.label);
                        }));
                }
            });

            var width = (parentHeight)*2.13,
            height = parentHeight,
            radius = Math.min(width, height) / 2;

            svg
                .attr("width", width)
                .attr("height", height);

            var g = svg.append("g")
                .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
            g.append("g")
                .attr("class", "slices");
            g.append("g")
                .attr("class", "labels");
            g.append("g")
                .attr("class", "lines");
            g.append("g")
                .attr("class", "title");

            var pie = d3.layout.pie()
                .sort(null)
                .value(function(d) {

                    for (var i = 0; i < PieChart.LabeledInstance.length; i++) {
                            //alert(PieChart.LabeledInstance[i].measurement)
                        if(PieChart.LabeledInstance[i].label == d.label)
                            return Math.abs(PieChart.LabeledInstance[i].measurement.value);
                    };
                });

            var arc = d3.svg.arc()
                .outerRadius(radius * 0.8)
                .innerRadius(radius * 0.4);

            var outerArc = d3.svg.arc()
                .innerRadius(radius * 0.9)
                .outerRadius(radius * 0.9);

            var key = function(d){ return d.data.label; };

            function mergeWithFirstEqualZero(first, second){
                var secondSet = d3.set(); second.forEach(function(d) { secondSet.add(d.label); });

                var onlyFirst = first
                    .filter(function(d){ return !secondSet.has(d.label) })
                    .map(function(d) { return {label: d.label, value: 0}; });
                return d3.merge([ second, onlyFirst ])
                    .sort(function(a,b) {
                        return d3.ascending(a.label, b.label);
                    });
            }

            function change(data) {
                //var duration = +document.getElementById("duration").value;
                var data0 = svg.select(".slices").selectAll("path.slice")
                    .data().map(function(d) { return d.data });
                if (data0.length == 0) data0 = data;
                var was = mergeWithFirstEqualZero(data, data0);
                var is = mergeWithFirstEqualZero(data0, data);

                /* ------- SLICE ARCS -------*/

                var slice = svg.select(".slices").selectAll("path.slice")
                    .data(pie(was), key);

                slice.enter()
                    .insert("path")
                    .attr("class", "slice")
                    .style("fill", function(d) { return color(d.data.label); })
                    .each(function(d) {
                        this._current = d;
                    });

                slice = svg.select(".slices").selectAll("path.slice")
                    .data(pie(is), key);

                slice       
                    .transition().duration(1000)
                    .attrTween("d", function(d) {
                        var interpolate = d3.interpolate(this._current, d);
                        var _this = this;
                        return function(t) {
                            _this._current = interpolate(t);
                            return arc(_this._current);
                        };
                    });

                slice = svg.select(".slices").selectAll("path.slice")
                    .data(pie(data), key);

                slice
                    .exit().transition().delay(1000).duration(0)
                    .remove();

                /* ------- TEXT LABELS -------*/

                var text = svg.select(".labels").selectAll("text")
                    .data(pie(was), key);

                text.enter()
                    .append("text")
                    .style("font-size", height/12 + "px")
                    .attr("dy", ".35em")
                    .style("opacity", 0)
                    .text(function(d) {
                        return d.data.label;
                    })
                    .each(function(d) {
                        this._current = d;
                    });
                
                function midAngle(d){
                    return d.startAngle + (d.endAngle - d.startAngle)/2;
                }

                text = svg.select(".labels").selectAll("text")
                    .data(pie(is), key);

                text.transition().duration(1000)
                    .style("opacity", function(d) {
                        return d.data.value == 0 ? 0 : 1;
                    })
                    .attrTween("transform", function(d) {
                        var interpolate = d3.interpolate(this._current, d);
                        var _this = this;
                        return function(t) {
                            var d2 = interpolate(t);
                            _this._current = d2;
                            var pos = outerArc.centroid(d2);
                            pos[0] = radius * (midAngle(d2) < Math.PI ? 1 : -1);
                            return "translate("+ pos +")";
                        };
                    })
                    .styleTween("text-anchor", function(d){
                        var interpolate = d3.interpolate(this._current, d);
                        return function(t) {
                            var d2 = interpolate(t);
                            return midAngle(d2) < Math.PI ? "start":"end";
                        };
                    });
                
                text = svg.select(".labels").selectAll("text")
                    .data(pie(data), key);

                text
                    .exit().transition().delay(1000)
                    .remove();

                /* ------- SLICE TO TEXT POLYLINES -------*/

                var polyline = svg.select(".lines").selectAll("polyline")
                    .data(pie(was), key);
                
                polyline.enter()
                    .append("polyline")
                    .style("opacity", 0)
                    .each(function(d) {
                        this._current = d;
                    });

                polyline = svg.select(".lines").selectAll("polyline")
                    .data(pie(is), key);
                
                polyline.transition().duration(1000)
                    .style("opacity", function(d) {
                        return d.data.value == 0 ? 0 : .5;
                    })
                    .attrTween("points", function(d){
                        this._current = this._current;
                        var interpolate = d3.interpolate(this._current, d);
                        var _this = this;
                        return function(t) {
                            var d2 = interpolate(t);
                            _this._current = d2;
                            var pos = outerArc.centroid(d2);
                            pos[0] = radius * 0.95 * (midAngle(d2) < Math.PI ? 1 : -1);
                            return [arc.centroid(d2), outerArc.centroid(d2), pos];
                        };          
                    });
                
                polyline = svg.select(".lines").selectAll("polyline")
                    .data(pie(data), key);
                
                polyline
                    .exit().transition().delay(1000)
                    .remove();


                svg.select(".title").select("text")
                    .remove();
                svg.select(".title")
                   .attr("transform", "translate("+(width*0.4)+",-"+(height*0.4)+")")
                   .append("text")
                        .attr("y", 6)
                        .attr("dy", ".71em")
                        .style("text-anchor", "end")
                        .text(PieChart.config.label);
            };
        }
    }    
});

