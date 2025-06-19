import { Component, HostListener } from '@angular/core';
import { ProductModel } from '../models/product';
import { Product } from "../product/product";
import { CartItem } from '../models/cartItem';
import { ProductService } from '../services/productService';
import { FormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-products',
  imports: [Product, FormsModule],
  templateUrl: './products.html',
  styleUrl: './products.css'
})
export class Products {

  products: ProductModel[] =[];
  cartCount: number = 0;
  carItems: CartItem[] = [];
  searchString: string = "";
  searchSubject= new Subject<string>();
  loading: boolean = true;
  skip: number = 0;
  limit: number = 10;
  total: number = 0;

  constructor(private productService: ProductService) {

  }

  handleAddToCart(id: Number) {
    console.log("Handling item - " + id)
    let flag = false;

    for (let i = 0; i < this.carItems.length; i++) {
      if (this.carItems[i].id == id) {
        this.carItems[i].count++;
        flag = true;
      }
    }

    if (!flag) {
      this.carItems.push(new CartItem(id, 1))
    }
    this.cartCount++;
  }

  handleSearch() {
  //  this.productService.getSearchProducts(this.searchString).subscribe({
  //   next: (data: any) => {
  //     console.log(this.searchString);
  //     this.products = data.products;
  //   }
  // })
  this.searchSubject.next(this.searchString);
  }

  ngOnInit(): void {
    // this.productService.getProducts().subscribe(
    //   {
    //     next: (data: any) => {
    //       this.products = data.products as ProductModel[];
    //     },
    //     error: (err) => {
    //       console.error(err)
    //     },
    //     complete: () => { }
    //   }
    // )
    this.searchSubject.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      tap(() => this.loading = true),
      switchMap(query=>this.productService.getSearchProducts(query, this.limit, this.skip)),
                tap(() => this.loading = false))
      .subscribe({
        next:(data:any)=>{
           this.products = data.products as ProductModel[];
           this.total = data.total;
        }
      });

      
    }
    @HostListener('window:scroll', [])
    onScroll(): void{

      const scrollPosition: number = window.innerHeight + window.scrollY;
      const thershold: number = document.body.offsetHeight - 100;

      if(scrollPosition >= thershold && this.products?.length < this.total){
        this.loadMore()
      }

    }

    loadMore(){
      this.loading = true
      this.skip += this.limit;
      this.productService.getSearchProducts(this.searchString, this.limit, this.skip).subscribe({
        next: (data: any) => {
          this.products = [...this.products, ...data.products]
          this.loading =false;
        }
      })

    }
}
