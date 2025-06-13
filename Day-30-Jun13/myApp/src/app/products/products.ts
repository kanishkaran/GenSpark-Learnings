import { Component } from '@angular/core';
import { ProductModel } from '../models/product';
import { Product } from "../product/product";
import { CartItem } from '../models/cartItem';
import { ProductService } from '../services/productService';

@Component({
  selector: 'app-products',
  imports: [Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products: ProductModel[] | undefined = undefined;
  cartCount: number = 0;
  carItems : CartItem[] = [];

  constructor(private productService: ProductService) {

  }

  handleAddToCart(id: Number){
    console.log("Handling item - "+ id)
    let flag = false;

    for(let i = 0; i < this.carItems.length; i++){
      if(this.carItems[i].id == id){
        this.carItems[i].count++;
        flag = true;
      }
    }

    if(!flag){
      this.carItems.push(new CartItem(id, 1))
    }
    this.cartCount++;
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
