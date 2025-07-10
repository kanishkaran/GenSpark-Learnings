import { Component, inject, Input } from '@angular/core';
import { ProductModel } from '../models/product';
import { CurrencyPipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { productService } from '../services/productService';

@Component({
  selector: 'app-product',
  imports: [CurrencyPipe],
  templateUrl: './product.html',
  styleUrl: './product.css'
})
export class Product {
@Input() product: ProductModel = new ProductModel();
router = inject(ActivatedRoute)
productService = inject(productService)

id : number = 0;

  ngOnInit(){
    this.id = this.router.snapshot.params['id'] as number;
    this.productService.getProductsById(this.id).subscribe({
      next:(data: any) => {
        this.product = data;
      }
    })
  }
}
