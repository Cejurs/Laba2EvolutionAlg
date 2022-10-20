var chart = null;
var solutionFind = false;
function Evolve() {
    $.ajax({
        type: "POST",
        url: "/Home/Evolve",
        data: "",
        success: function () {
            UpdateChartAndTable();
        },
        error: function () {
            console.log("Error");
        }
    });
}
window.onload = function () {
    CreateChart();
    SendPopulation(document.getElementById("sendBtn"));
}
function SendPopulation(btnClicked) {
    var $form = $(btnClicked).parents('form');
    $.ajax({
        type: "POST",
        url: "/Home/CreatePopulation",
        data: $form.serialize(),
        success: function (response) {
            UpdateChartAndTable();
        }
    });
    solutionFind = false;
    return false;// if it's a link to prevent post

}
function CreateChart()
{
    $('#ChartArea').append('<canvas id="myChart" > </canvas>');
    $.ajax({
        type: "GET",
        url: "/Home/GerFunctionPoints",
        data: "",
        contextType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessResult,
        error: OnError
    });

    function OnSuccessResult(data) {
        console.log(data);
        let _data = data;
        let _chartLabels = _data[0];
        let _chartData = _data[1];
        let _color = _data[2];
        chart=new Chart("myChart", {
            type: "line",
            data: {
                labels: _chartLabels,
                datasets: [{
                    backgroundColor: _color,
                    data: _chartData
                }]
            }
        });

    }
    function OnError(error) {

    }
}
function UpdateChartAndTable() {

    $.ajax({
        type: "GET",
        url: "/Home/GetIndividualPoints",
        data: "",
        contextType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccessResult,
        error: OnError
    });

    function OnSuccessResult(data) {
        console.log(data);
        let _data = data;
        let _point = data[0];
        let _individuals = data[1];
        solutionFind = data[2];
        $("#table").remove();
        $("#tableArea").append('<table id="table"></table>');
        $("#tableArea>table").append('<tr><th>Особи</th></tr>');
        _individuals.forEach(function (item, i, arr) {
            $('#tableArea > table').append('<tr><td>' + item + '</td></tr>');
        });

        ClearChart()
        _point.forEach(function (item, i, arr) {
            chart.data.datasets[0].backgroundColor[item]="red";
        });
        chart.update();
    }
    function OnError(error) {

    }
}

async function WhileEvolve() {
    document.getElementById("NextGeneration").disabled = true;
    document.getElementById("sendBtn").disabled = true;
    $.ajax({
        type: "POST",
        url: "/Home/Evolve",
        data: "",
        success: function () {
            UpdateChartAndTable();
            if (!solutionFind) {
                sleep(200);
                WhileEvolve();
            }
            else {
                document.getElementById("NextGeneration").disabled = false;
                document.getElementById("sendBtn").disabled = false;
            }
        },
        error: function () {
            console.log("Error");
        }
    });
    
}
function sleep(milliseconds) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}
function ClearChart() {
    chart.data.datasets[0].backgroundColor.forEach(function (item, i, arr) {
        arr[i] = "blue"
    })
    chart.update();
}