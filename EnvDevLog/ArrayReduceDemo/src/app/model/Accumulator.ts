import { Forecast } from "./OpenWeatherMap";

export interface Accumulator {
    date: string;
    forecasts: Forecast[];
}