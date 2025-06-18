import { Routes } from '@angular/router';
import { UserDetails } from './user-details/user-details';
import { AddUser } from './add-user/add-user';
import { ChartComponent } from './chart-component/chart-component';

export const routes: Routes = [
    {'path': 'users', component: UserDetails},
    {'path': 'add-user', component: AddUser},
    {'path' : 'charts', component: ChartComponent}
];
