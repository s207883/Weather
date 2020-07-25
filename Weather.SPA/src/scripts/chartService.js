import "chart.js";

const dataColors = [
    'rgba(255, 99, 132, 0.6)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)'
];

const borderColors = [
    'rgba(255, 99, 132, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)'
]

/**
 * Функция рисования графиков.
 * @param {HTMLElement} chartElement Элемент canvas для отображения графика
 * @param {
  {
     labels: Array<string>,
     datasets: [
         {
             label: 'data_label',
             data: Array<Number>,
             backgroundColor: dataColors,
             borderColor: borderColors,
             borderWidth: Number}
            ]
        }
    } chartData Данные для отображения на графике
    
 * @param {string} chartType по-умолчанию 'bar'
 */
const drawChart = (chartElement, chartData, chartType = 'bar') => {
    new Chart(chartElement, {
        type: chartType,
        data: {
            labels: chartData.labels,
            datasets: chartData.datasets,
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

const chartElement = document.getElementById('weatherChart');
drawChart(chartElement, {
    labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
    datasets: [{
        label: '# of Votes',
        data: [12, 19, 3, 10, 2, 3],
        backgroundColor: dataColors[0],
        borderColor: borderColors[1],
        borderWidth: 1
    }, {
        label: '# of Votes',
        data: [15, 26, 15, -15, -2, 3],
        backgroundColor: dataColors[1],
        borderColor: borderColors[1],
        borderWidth: 1
    }]
});