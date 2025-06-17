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

    getSearchProducts(searchBy: string, limit: number=10, skip: number= 10){
        return this.http.get<any[]>(`https://dummyjson.com/products/search?q=${searchBy}&limit=${limit}&skip=${skip}`);
    }
}