import { HttpClient } from "@angular/common/http";


export class weatherService{

    constructor(private http : HttpClient){

    }

    getWeatherData(city : string){
        var result = this.http.get("https://api.openweathermap.org/data/2.5/weather?q=chennai&appid=fa88617dcb6406b442a1d622105a8b88");
        console.log(result)
    }
}