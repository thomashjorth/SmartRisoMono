google.load('visualization', '1.1', { packages: ['line', 'corechart'] });
var chart, data, options, count = 0;
function drawGraph() {
    data = new google.visualization.DataTable();
    data.addColumn('number', 'Month');
    data.addColumn('number', "Average Temperature");

    options = {
        title: 'Average Temperatures and Daylight in Iceland Throughout the Year',
        //width: 600, height: 300,
        
        legend: { position: 'top' },
        vAxes: {
            // Adds titles to each axis.
            0: { title: 'Temps (Celsius)' }
        },
        hAxes: {
            // Adds titles to each axis.
            0: { title: 'Day' }
        },
        vAxis: {
            viewWindow: {
                max: 30
            }
        }
    };

    chart = new google.visualization.LineChart(document.getElementsByClassName('graph')[0]);

    chart.draw(data, options);
}

function updateValueGraph(newValue) {
    count++;
    if(count>20 && data.getNumberOfRows > 0)
        data.removeRow(0);
    else if (data.getNumberOfRows === 0)
        count = 0;
    data.addRow([count, newValue]);
    chart.draw(data, options);
}