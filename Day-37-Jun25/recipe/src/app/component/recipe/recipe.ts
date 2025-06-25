import { Component, Input, OnInit } from '@angular/core';
import { recipeModel } from '../../models/recipe.model';

import { RecipeService } from '../../service/recipe-service';

@Component({
  selector: 'app-recipe',
  imports: [],
  templateUrl: './recipe.html',
  styleUrl: './recipe.css'
})
export class Recipe{

@Input() recipe: recipeModel | null = null;

// constructor(private recipeService: RecipeService){}

// ngOnInit(): void {
//   this.recipeService.getRecipe(1).subscribe({
//     next: (data : any) => {
//       this.recipe = data;
//     }
//   })
// }
}
