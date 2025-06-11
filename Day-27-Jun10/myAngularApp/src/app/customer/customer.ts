import { Component } from '@angular/core';


@Component({
  selector: 'app-customer',
  imports: [],
  templateUrl: './customer.html',
  styleUrl: './customer.css'
})
export class Customer {
  uname: string;
  phoneNumber: string;
  age: string;
  className: string = "card-off";
  likeCount: number = 0;
  dislikeCount: number = 0;

  constructor() {
    this.uname = "Name";
    this.phoneNumber = "Phone Number";
    this.age = "0";
  }

  displayCustomer(uname: string, phone: string, age: string) {
    this.uname = uname;
    this.phoneNumber = phone;
    this.age = age;
    this.className = "card-on";
  }

  like() {
    this.likeCount++;
  }

  dislike() {
    this.dislikeCount++;
  }
}