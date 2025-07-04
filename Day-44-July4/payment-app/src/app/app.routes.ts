import { Routes } from '@angular/router';
import { PaymentForm } from './components/payment-form/payment-form';
import { SuccessPage } from './components/success-page/success-page';
import { FailurePage } from './components/failure-page/failure-page';


export const routes: Routes = [
    {path: 'payment', component: PaymentForm},
    {path: 'payment-success', component: SuccessPage},
    {path: 'payment-failure', component: FailurePage}
];
