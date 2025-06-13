import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class ProductService{
    private http = inject(HttpClient)

    getProduct(id: number){
        return this.http.get(`https://dummyjson.com/products/${id}`)
    }

    getProducts():Observable<any[]>{
        return this.http.get<any[]>("https://dummyjson.com/products");
    }
}