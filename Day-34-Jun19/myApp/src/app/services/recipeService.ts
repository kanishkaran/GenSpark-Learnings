import { inject, Injectable } from "@angular/core";
import { recipeModel } from "../models/recipeModel";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";


@Injectable()
export class RecipeService{
    private http = inject(HttpClient);

    getRecipe(id: number):Observable<recipeModel>{
        return this.http.get<recipeModel>(`https://dummyjson.com/recipes/${id}`);
    }

    getAllRecipe(): Observable<recipeModel[]>{
        return this.http.get<recipeModel[]>("https://dummyjson.com/recipes");
    }
}