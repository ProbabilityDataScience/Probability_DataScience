<html>
<head>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', {'packages':['corechart']});
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {

            var graphState = new Array();
            var money = new Array();

            graphState.push(1);
            money.push(2);


            var finalGraphData = [['d', '1'], [1, 2]];

            var myArray = [
                ['Year', 'Sales', 'Expenses'],
                ['2013',  1050,      400],
                ['2014',  1170,      460],
                ['2015',  660,       1120],
                ['2016',  1030,      540]
            ];

            var myArray2 = new Array(new Array(4), new Array(4), new Array(4), new Array(4));
//new Array(new Array(5)), new Array(new Array(5)), new Array(new Array(5))

            myArray2[0][0] = 'Year';
            myArray2[1][0] = 'Sales';
            myArray2[2][0] = 'Tod';
            myArray2[3][0] = 'PRe';

            myArray2[0][1] = '2013';
            myArray2[1][1] = 1050;
            myArray2[2][1] = 2050;
            myArray2[3][1] = 1050;

            myArray2[0][2] = '2014';
            myArray2[1][2] = 750;
            myArray2[2][2] = 4150;
            myArray2[3][2] = 3150;

            myArray2[0][3] = '2015';
            myArray2[1][3] = 2170;
            myArray2[2][3] = 1170;
            myArray2[3][3] = 1670;

            var data = google.visualization.arrayToDataTable(myArray2);

            var options = {
                title: 'Company Performance',
                hAxis: {title: 'Year',  titleTextStyle: {color: '#333'}},
                vAxis: {minValue: 0}
            };

            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            chart.draw(data, options);
        }
    </script>
</head>
<body>
<div id="chart_div" style="width: 100%; height: 500px;"></div>
</body>
</html>
