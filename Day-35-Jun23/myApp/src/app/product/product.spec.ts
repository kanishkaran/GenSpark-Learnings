import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Product } from './product';
import { ProductModel } from '../models/product';
import { Component } from '@angular/core';
import { ProductService } from '../services/productService';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';

class MockProductService{
  getProduct(id: number){
    return { subscribe: () => {}}
  }
}

@Component({
  standalone: true,
  imports: [Product],
  template: `<app-product [product]=product (addToCart)="onAdd($event)"></app-product>`
})
class HostComponent{
  product: ProductModel = new ProductModel();
  productId: number | null = null;

  onAdd(id: number){
    this.productId = id;
  }
}


describe('Product', () => {
  let component: Product;
  let fixture: ComponentFixture<HostComponent>;
  let hostComponent: HostComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HostComponent],
      providers: [{provide: ProductService, useClass: MockProductService}, provideHttpClient(), provideHttpClientTesting()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HostComponent);
    hostComponent = fixture.componentInstance;
    fixture.detectChanges();
  });

   it('check render product object input',()=>{
    hostComponent.product = {
      id:1,
      title:'Abc',
      price:90,
      description:'blah blah',
    } as ProductModel;
     fixture.detectChanges();
     const compiled = fixture.nativeElement as HTMLElement;
     expect(compiled.textContent).toContain('blah blah');
    })
});
