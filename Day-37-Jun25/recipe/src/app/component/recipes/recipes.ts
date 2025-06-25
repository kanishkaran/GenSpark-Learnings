import { Component, OnInit } from '@angular/core';
import { RecipeService } from '../../service/recipe-service';
import { recipeModel } from '../../models/recipe.model';
import { Recipe } from "../recipe/recipe";
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject, switchMap } from 'rxjs';

@Component({
  selector: 'app-recipes',
  imports: [Recipe, FormsModule],
  templateUrl: './recipes.html',
  styleUrl: './recipes.css'
})
export class Recipes implements OnInit {
  recipes: recipeModel[] = [];
  searchTerm: string = ''
  searchSubject = new Subject<string>();

  constructor(private recipeService: RecipeService) { }


  ngOnInit(): void {
    this.recipeService.getAllRecipes().subscribe({
      next: (data: any) => {
        this.recipes = data.recipes;
      }
    })

    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap(query => this.recipeService.getSearchResult(query))
    ).subscribe(
      (data: any) => {
        this.recipes = data.recipes
      }
    )
  }

  handleSearch() {
    // this.recipeService.getSearchResult(this.searchTerm).subscribe(( data : any) => {
    //   this.recipes = data.recipes;
    // })
    this.searchSubject.next(this.searchTerm);
  }

}
