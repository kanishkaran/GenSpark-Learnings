import { HttpClient } from "@angular/common/http"
import { inject } from "@angular/core";



export class productService{

    http = inject(HttpClient)

    getProductsBySearch(searchData: string, skip: number = 10){
        return this.http.get<any[]>(`https://dummyjson.com/products/search?q=${searchData}&limit=10&skip=${skip}`);
    }

    getAllProducts(){
        return this.http.get<any[]>(`https://dummyjson.com/products/`);
    }

    getProductsById(id: number){
        return this.http.get<any[]>(`https://dummyjson.com/products/${id}`);
    }
}