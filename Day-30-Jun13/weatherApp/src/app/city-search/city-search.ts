import { Component } from '@angular/core';
import { WeatherService } from '../services/weatherService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-city-search',
  imports: [FormsModule],
  templateUrl: './city-search.html',
  styleUrl: './city-search.css'
})
export class Citysearch {
cityName: string = ""
constructor(private weatherService: WeatherService){

}

  handleSearchWeather(){
    this.weatherService.getWeatherData(this.cityName).subscribe({
      next: (data) => {
        this.weatherService.setWeatherData(data);
        console.log(data);
      },
      error: (data) =>{
        alert("Enter Valid City Name")
      }


    })
  }
}