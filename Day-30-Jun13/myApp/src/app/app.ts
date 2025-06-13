import { Component } from '@angular/core';
import { Citysearch } from "./citysearch/citysearch";


@Component({
  selector: 'app-root',
  imports: [Citysearch],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'myApp';
}
