import "chart.js";

export const dataColors = [
    'rgba(255, 99, 132, 0.6)',
    'rgba(54, 162, 235, 0.2)',
    'rgba(255, 206, 86, 0.2)',
    'rgba(75, 192, 192, 0.2)',
    'rgba(153, 102, 255, 0.2)',
    'rgba(255, 159, 64, 0.2)'
];

export const borderColors = [
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
             labels: string[],
             data: Number[],
             backgroundColor: dataColors,
             borderColor: borderColors,
             borderWidth: Number}
            ]
        }
    } chartData Данные для отображения на графике
    
 * @param {string} chartType по-умолчанию 'bar'
 */
export const drawChart = (chartElement, chartData, chartType = 'bar') => {
    var chart = new Chart(chartElement, {
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

    return chart;
}