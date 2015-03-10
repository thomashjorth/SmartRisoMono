var GaugeController = function ($scope) {
    $.getScript("../Visualization/Gauge.js", function () { });

    drawGauge(-2, 2, "Power");
    updateValue(-0.44);
}

GaugeController.$inject = ['$scope'];