import '../styles/style.css';

import { drawChart, dataColors, borderColors } from "./chartService";

import { getWeatherByCoordinatesAndDate } from './apiRequestService';

const weatherRequestForm = document.getElementById('weather_form');

let chart = undefined;

weatherRequestForm.onsubmit = (async (event) => {
    event.preventDefault();
    var latitude = document.getElementById('latitude').value;
    var longitude = document.getElementById('longitude').value;
    var dateFrom = document.getElementById('dateFrom').valueAsDate.toLocaleDateString("en-US");
    var dateTo = document.getElementById('dateTo').valueAsDate.toLocaleDateString("en-US");

    if (latitude != "" & longitude != "" & dateFrom != "" & dateTo != "") {
        var result = await getWeatherByCoordinatesAndDate({ latitude, longitude, dateFrom, dateTo })
        if (result.isSuccess) {
            const weatherData = result.data.weatherData;
            const weatherTimeRange = result.data.timeRange;

            const dateFromInput = document.getElementById('dateFrom');
            const dateToInput = document.getElementById('dateTo');

            dateFromInput.valueAsDate = new Date(weatherTimeRange.startDate.slice(0, 10));
            dateToInput.valueAsDate = new Date(weatherTimeRange.endDate.slice(0, 10));

            let chartData = {
                labels: [],
                datasets: [{
                    label: 'Temperature',
                    data: [],
                    backgroundColor: dataColors[0],
                    borderColor: borderColors[0],
                    borderWidth: 1,
                },
                {
                    label: 'Humidity %',
                    data: [],
                    backgroundColor: dataColors[1],
                    borderColor: borderColors[1],
                    borderWidth: 1,
                },
                {
                    label: 'Precipitation in mm',
                    data: [],
                    backgroundColor: dataColors[2],
                    borderColor: borderColors[2],
                    borderWidth: 1,
                }],
            };

            for (const item of weatherData) {
                chartData.labels.push(new Date(item.weatherDate).toDateString());
                chartData.datasets[0].data.push(item.temperature);
                chartData.datasets[1].data.push(item.humidity);
                chartData.datasets[2].data.push(item.precipitation);
            }

            const chartElement = document.getElementById('weatherChart');

            if (chart != null && typeof (chart.destroy) === 'function') {
                chart.destroy();
            }
            chart = drawChart(chartElement, chartData);
        }
    }
});

weatherRequestForm.onreset = (async (_) => {
    if (chart != null && typeof (chart.destroy) === 'function') {
        chart.destroy();
    }
})