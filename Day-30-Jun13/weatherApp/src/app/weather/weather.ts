import { Component, inject,  Injectable } from '@angular/core';
import { Citysearch } from "../city-search/city-search";
import { WeatherService } from '../services/weatherService';
import { Weathercard } from "../weathercard/weathercard";
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-weather',
  imports: [Citysearch, Weathercard, AsyncPipe],
  templateUrl: './weather.html',
  styleUrl: './weather.css'
})
@Injectable()
export class Weather {
  weatherService = inject(WeatherService);
  
  weather$ : Observable<any> = this.weatherService.weather$;

}
