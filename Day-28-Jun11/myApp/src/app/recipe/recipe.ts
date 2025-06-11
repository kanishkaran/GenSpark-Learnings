import { Component, Input } from '@angular/core';
import { recipeModel } from '../models/recipeModel';
import { RecipeService } from '../services/recipeService';

@Component({
  selector: 'app-recipe',
  imports: [],
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe {
@Input() recipe: recipeModel | undefined = undefined;

  // constructor(private recipeService: RecipeService){

  // }

  // ngOnInit(){
  //   this.recipeService.getRecipe(1).subscribe({
  //     next: (data) => {
  //       this.recipe = data;
  //     },
  //     error: (err) => {
  //       console.error(err);
  //     },
  //     complete: () => {
  //       console.log("Job Done !")
  //     }
  //   })
  // }
}
