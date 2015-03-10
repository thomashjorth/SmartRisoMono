var GraphController = function ($scope) {
    $.getScript("../Visualization/Graph.js", function () {});

    drawGraph();
    updateValueGraph(1);
    updateValueGraph(4);
    updateValueGraph(5);
    updateValueGraph(2);
    updateValueGraph(3);
    updateValueGraph(0);
    updateValueGraph(2);
    updateValueGraph(7);
    updateValueGraph(4);
    updateValueGraph(5);
    updateValueGraph(3);
    updateValueGraph(6);
    updateValueGraph(2);
    updateValueGraph(3);
    updateValueGraph(1);
    updateValueGraph(5);
    updateValueGraph(2);
    updateValueGraph(3);
    updateValueGraph(3);
    updateValueGraph(4);
    updateValueGraph(5);
    updateValueGraph(0);
}

GraphController.$inject = ['$scope'];
