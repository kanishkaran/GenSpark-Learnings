import { Component } from '@angular/core';
import { Recipes } from "./component/recipes/recipes";

@Component({
  selector: 'app-root',
  imports: [Recipes],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'recipe';
}
