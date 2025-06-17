import { Component, HostListener } from '@angular/core';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';
import { ProductModel } from '../models/product';
import { productService } from '../services/productService';
import { FormsModule } from '@angular/forms';
import { Product } from '../product/product';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  imports: [FormsModule, Product],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {
  products: ProductModel[] = [];
  searchTerm: string = "";
  searchSubject = new Subject<string>();
  limit: number = 10;
  skip: number = 0;
  loading: boolean = true;
  total: number = 0;
  showBackToTop: boolean = false;

  constructor(private productService: productService, private router: Router) {

  }

  handleSearch() {
    this.searchSubject.next(this.searchTerm);
  }

  ngOnInit() {
    this.productService.getProductsBySearch('', this.skip)
      .subscribe({
        next: (data: any) => {
          this.products = [...this.products, ...data.products]
          this.loading = false;
        },
        error: (data : any) => {
          this.router.navigate(['detail', data.message])
        }
      })

    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query => this.productService.getProductsBySearch(query, this.skip)),
      tap(() => this.loading = false)).subscribe({
        next: (data: any) => {
          this.products = data.products as ProductModel[];
          this.total = data.total;

        },
        error: (data : any) => {
          this.router.navigate(['detail', data.message])
        }
      });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {


    this.showBackToTop = window.scrollY > 300;
    const scrollPosition = window.innerHeight + window.scrollY;
    const threshold = document.body.offsetHeight - 100;
    if (scrollPosition >= threshold && this.products?.length < this.total) {
      console.log(scrollPosition);
      console.log(threshold)

      this.loadMore();
    }
  }
  loadMore() {
    this.loading = true;
    this.skip += this.limit;
    this.productService.getProductsBySearch(this.searchTerm, this.skip)
      .subscribe({
        next: (data: any) => {
          this.products = [...this.products, ...data.products]
          this.loading = false;
        }
      })
  }

  backToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }


}
