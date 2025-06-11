import { Component, signal } from '@angular/core';
import { recipeModel } from '../models/recipeModel';
import { RecipeService } from '../services/recipeService';
import { Recipe } from "../recipe/recipe";

@Component({
  selector: 'app-recipes',
  imports: [Recipe],
  templateUrl: './recipes.html',
  styleUrl: './recipes.css'
})
export class Recipes {
  recipes = signal<recipeModel[]>([])

  constructor(private recipeService: RecipeService) {

  }

  ngOnInit() {
    this.recipeService.getAllRecipe().subscribe(
      {
        next: (data: any) => this.recipes.set(data.recipes),
        error: (err) => {
          console.error(err);
        },
        complete: () => { 
          console.log("Collected all recipes")
        }

      })
  }
}
