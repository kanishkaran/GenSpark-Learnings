import { Routes } from '@angular/router';
import { Products } from './products/products';
import { Login } from './login/login';

export const routes: Routes = [
    {"path": 'products', component:Products},
    {'path': 'login', component:Login}
];
