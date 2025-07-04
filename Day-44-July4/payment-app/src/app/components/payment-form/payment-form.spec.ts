import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PaymentForm } from './payment-form';
import { RazorpayService } from '../../services/payment.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';


describe('PaymentForm', () => {
  let component: PaymentForm;
  let fixture: ComponentFixture<PaymentForm>;
  let mockRazorpayService: jasmine.SpyObj<RazorpayService>;
  let mockRouter: jasmine.SpyObj<Router>;

  beforeEach(() => {
    mockRazorpayService = jasmine.createSpyObj('RazorpayService', ['openPayment']);
    mockRouter = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      providers: [
        { provide: RazorpayService, useValue: mockRazorpayService },
        { provide: Router, useValue: mockRouter },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(PaymentForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create the payment form component', () => {
    expect(component).toBeTruthy();
  });

  it('should have required form controls invalid initially', () => {
    expect(component.paymentForm.valid).toBeFalse();
    expect(component.amount?.valid).toBeFalse();
    expect(component.name?.valid).toBeFalse();
    expect(component.email?.valid).toBeFalse();
    expect(component.contact?.valid).toBeFalse();
  });

  it('should mark form as valid with proper input values', () => {
    component.paymentForm.setValue({
      amount: 100,
      name: 'Test',
      email: 'test@example.com',
      contact: '9876543210',
    });
    expect(component.paymentForm.valid).toBeTrue();
  });

  it('should not call Razorpay if form is invalid', () => {
    component.paymentForm.setValue({
      amount: null,
      name: '',
      email: 'bademail',
      contact: '',
    });
    component.onSubmit();
    expect(mockRazorpayService.openPayment).not.toHaveBeenCalled();
  });

  it('should call RazorpayService.openPayment with correct data', () => {
    component.paymentForm.setValue({
      amount: 100,
      name: 'Test',
      email: 'test@example.com',
      contact: '9876543210',
    });

   
    mockRazorpayService.openPayment.and.callFake((options: any) => {
      options.onSuccess();
    });

    component.onSubmit();

    expect(mockRazorpayService.openPayment).toHaveBeenCalled();

    expect(mockRouter.navigate).toHaveBeenCalledWith(['payment-success']);
  });

  it('should navigate to failure page on Razorpay cancel', () => {
    component.paymentForm.setValue({
      amount: 999,
      name: 'Test User',
      email: 'test@user.com',
      contact: '1234567890',
    });

    mockRazorpayService.openPayment.and.callFake((options: any) => {
      options.onFailure();
    });

    component.onSubmit();
    expect(mockRouter.navigate).toHaveBeenCalledWith(['payment-failure']);
  });
});
