import { Component, inject, Input } from '@angular/core';
import { ProductModel } from '../models/product';
import { ProductService } from '../services/productService';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
  @Input() product: ProductModel = new ProductModel();
  private productService = inject(ProductService);

  constructor(){
    // this.productService.getProduct(1).subscribe(
    //   {
    //     next: (data) => {
    //       this.product = data as ProductModel;
    //     },
    //     error: (err) => {
    //       console.log(err);
    //     },
    //     complete: () => {
    //       console.log("Finished the job ;)")
    //     }
        
    //   }
    // )
  }

}
