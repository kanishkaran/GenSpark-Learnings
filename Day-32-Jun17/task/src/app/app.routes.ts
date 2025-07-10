import { Routes } from '@angular/router';
import { Products } from './products/products';
import { Product } from './product/product';
import { AuthGuard } from './auth-guard';
import { Login } from './login/login';
import { Detail } from './detail/detail';

export const routes: Routes = [
    {'path': 'products', canActivate: []
        , children: [
        {'path': '', component: Products},
        {'path': ':id', component: Product, canActivate:[AuthGuard]}
    ]},
    {'path': 'login', component: Login},
    {'path': 'detail/:msg', component: Detail}
];
