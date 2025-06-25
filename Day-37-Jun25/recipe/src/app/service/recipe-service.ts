import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { recipeModel } from '../models/recipe.model';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  constructor(private http: HttpClient) { }

  getRecipe(id: number) : Observable<recipeModel>{
   return this.http.get<recipeModel>(`https://dummyjson.com/recipes/${id}`);
  }

  getAllRecipes() : Observable<recipeModel[]> {
    return this.http.get<recipeModel[]>(`https://dummyjson.com/recipes/`);
  }

  getSearchResult(searchTerm : string){
    return this.http.get(`https://dummyjson.com/recipes/search?q=${searchTerm}`)
  }
}
