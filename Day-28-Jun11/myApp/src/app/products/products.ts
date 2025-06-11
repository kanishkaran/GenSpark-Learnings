import { Component } from '@angular/core';
import { ProductModel } from '../models/product';
import { ProductService } from '../services/productService';
import { Product } from "../product/product";

@Component({
  selector: 'app-products',
  imports: [Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products: ProductModel[] | undefined = undefined;

  constructor(private productService: ProductService) {

  }

  ngOnInit(): void{
    this.productService.getProducts().subscribe(
      {
        next:(data:any)=>{
         this.products = data.products as ProductModel[];
        },
        error:(err)=>{
          console.error(err)
        },
        complete:()=>{}
      }
    )
  }
}
