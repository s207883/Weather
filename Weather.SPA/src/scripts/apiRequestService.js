import { apiRoutes, apiHost, apiPort } from "./apiConsts";

/**
 * Получить погоду по координатам и дате.
 * @param {{
 * latitude:Number,
 * longitude:Number,
 * dateFrom:Date,
 * dateTo:Date
 * }} requestData Данные запроса.
 */
export async function getWeatherByCoordinatesAndDate(requestData) {
    const route = `${apiRoutes.getHistory}?StartDate=${requestData.dateFrom}&EndDate=${requestData.dateTo}&Latitude=${requestData.latitude}&Longitude=${requestData.longitude}`;

    const options = {
        method: 'GET'
    };

    const response = await fetch(route, options);

    const result = await response.json();
    return result;
}