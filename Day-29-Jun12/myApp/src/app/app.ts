import { Component } from '@angular/core';
import { Async } from './async/async';

@Component({
  selector: 'app-root',
  imports: [Async],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'myApp';
}
