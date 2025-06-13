import { Component} from '@angular/core';
import { weatherService } from '../services/WeatherService';


@Component({
  selector: 'app-citysearch',
  imports: [],
  templateUrl: './citysearch.html',
  styleUrl: './citysearch.css'
})

export class Citysearch {
cityName: string = ""
constructor(private weatherService: weatherService){

}

  handleSearchWeather(){
    this.weatherService.getWeatherData("")
  }
}
