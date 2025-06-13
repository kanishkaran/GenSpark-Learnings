import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, catchError, Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WeatherService{
    weatherSubject = new BehaviorSubject<any>(null);
    weather$ = this.weatherSubject.asObservable();
    constructor(private http : HttpClient){

    }

    getWeatherData(city: string) : Observable<any>{
        return this.http.get<any>(`https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=fa88617dcb6406b442a1d622105a8b88`).pipe(
            catchError((err) => {
                this.weatherSubject.next(null);
                throw err;
            })
        )
        
    }

    setWeatherData(weather: any){
        this.weatherSubject.next(weather);
    }

    
}