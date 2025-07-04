import { Injectable } from '@angular/core';

declare var Razorpay: any;

@Injectable({
  providedIn: 'root',
})
export class RazorpayService {

  private razorpayKey = 'rzp_test_1DP5mmOlF5G5ag';
  constructor() { }

  openPayment(options: {
    amount: number;
    name: string;
    email: string;
    contact: number;
    onSuccess: () => void;
    onFailure?: () => void;
  }): void {
    const razorpayOptions: any = {
      key: this.razorpayKey,
      amount: options.amount * 100,
      currency: 'INR',
      name: options.name,
      description: 'Test Transaction',
      handler: options.onSuccess,
      prefill: {
        name: options.name,
        email: options.email,
        contact: options.contact,
      },
      theme: {
        color: '#3399cc',
      },
      method: {
        upi: true,
      },
      modal: {
        ondismiss: () => {
          if (options.onFailure) options.onFailure();
        },
      },
    };

    const rzp = new Razorpay(razorpayOptions);
    rzp.open();
  }
}
