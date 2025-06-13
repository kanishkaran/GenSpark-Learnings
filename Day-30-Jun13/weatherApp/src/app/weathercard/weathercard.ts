
import { Component, Input } from '@angular/core';


@Component({
  selector: 'app-weathercard',
  imports: [],
  templateUrl: './weathercard.html',
  styleUrl: './weathercard.css'
})
export class Weathercard {
@Input() weather: any;
  
}
