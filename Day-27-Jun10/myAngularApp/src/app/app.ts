import { Component } from '@angular/core';
import { FirstComponent } from './first-component/first-component';
import { Customer } from './customer/customer';
import { Product } from './product/product';

@Component({
  selector: 'app-root',
  imports: [Customer, Product],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'myAngularApp';
}
