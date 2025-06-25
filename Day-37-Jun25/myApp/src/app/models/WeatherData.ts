import { Wind } from "./wind";

export class WeatherData{
    constructor(public temp: number = 0, public humidity: number = 0, public wind: Wind ){

    }
}