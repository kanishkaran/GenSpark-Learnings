import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';

import { Router } from '@angular/router';
import { RazorpayService } from '../../services/payment.service';


@Component({
  selector: 'app-payment-form',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './payment-form.html',
  styleUrl: './payment-form.css'
})
export class PaymentForm {
  paymentForm: FormGroup;

  constructor(private razorpayService: RazorpayService,
    private router: Router
  ) {
    this.paymentForm = new FormGroup({
      amount: new FormControl(null, [Validators.required]),
      name: new FormControl(null, [Validators.required, Validators.minLength(4)]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      contact: new FormControl(null, [
        Validators.required,
        Validators.minLength(10)
      ]),
    });
  }

  public get amount() {
    return this.paymentForm.get('amount');
  }

  public get name() {
    return this.paymentForm.get('name');
  }

  public get email() {
    return this.paymentForm.get('email');
  }

  public get contact() {
    return this.paymentForm.get('contact');
  }

  onSubmit() {
    if (this.paymentForm.invalid) return;

    const formValue = this.paymentForm.value;

    this.razorpayService.openPayment({
      amount: formValue.amount,
      name: formValue.name,
      email: formValue.email,
      contact: formValue.contact,
      onSuccess: () => {
        this.router.navigate(['payment-success'])
        console.log('success')
      },
      onFailure: () => {
        this.router.navigate(['payment-failure'])
      },
    });
  }
}
