import { ComponentFixture, fakeAsync, TestBed, tick } from '@angular/core/testing';

import { Products } from './products';
import { ProductService } from '../services/productService';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CartItem } from '../models/cartItem';

@Component({
  selector: 'app-product',
  template: ''
})
class MockProductComponent { }

describe('Products', () => {
  let component: Products;
  let fixture: ComponentFixture<Products>;
  let productServiceSpy: jasmine.SpyObj<ProductService>

  const dummyProductData: any = {
    products: [
      { id: 1, title: 'Phone' },
      { id: 2, title: 'Laptop' }
    ],
    total: 20
  };

  beforeEach(async () => {

    const spy = jasmine.createSpyObj('ProductService', ['getSearchProducts'])
    await TestBed.configureTestingModule({
      imports: [Products],
      providers: [{ provide: ProductService, useValue: spy }, provideHttpClient(), provideHttpClientTesting()],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();

    fixture = TestBed.createComponent(Products);
    component = fixture.componentInstance;
    productServiceSpy = TestBed.inject(ProductService) as jasmine.SpyObj<ProductService>;
    productServiceSpy.getSearchProducts.and.returnValue(of(dummyProductData));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('Should handle cart addition', () => {
    component.handleAddToCart(1);

    expect(component.carItems.length).toBe(1);
    expect(component.cartCount).toBe(1);
    expect(component.carItems[0].id).toBe(1);
    expect(component.carItems[0].count).toBe(1);
  })

  it('should increment cart items if items are already present', () => {
    component.carItems = [new CartItem(1, 2)]
    component.cartCount = 1;

    component.handleAddToCart(1);

    expect(component.carItems.length).toBe(1);
    expect(component.carItems[0].count).toBe(3);
    expect(component.cartCount).toBe(2);

  })

  it("should call loadMore method", () => {
    spyOn(component, 'loadMore');
    spyOnProperty(window, 'innerHeight').and.returnValue(1000);
    spyOnProperty(window, 'scrollY').and.returnValue(900);
    Object.defineProperty(document.body, 'offsetHeight', { value: 1800 });

    component.products = Array(10).fill({});
    component.total = 20;
    window.dispatchEvent(new Event('scroll'));
    expect(component.loadMore).toHaveBeenCalled()
  })

  it('should debounce and search products', fakeAsync(() => {
    component.searchString = 'laptop';
    component.handleSearch();

    tick(5000); 
    fixture.detectChanges();

    expect(productServiceSpy.getSearchProducts).toHaveBeenCalledWith('laptop', 10, 0);
    expect(component.products.length).toBe(2);
    expect(component.total).toBe(20);
  }));

  it('should load more products and update skip', () => {
    const newProducts: any = {
      products: [{ id: 3, title: 'Camera' }],
      total: 20
    };
    productServiceSpy.getSearchProducts.and.returnValue(of(newProducts));

    component.products = [];
    component.searchString = '';
    component.skip = 0;

    component.loadMore();
    expect(productServiceSpy.getSearchProducts).toHaveBeenCalledWith('', 10, 10);
    expect(component.loading).toBeFalse();
  });
});
