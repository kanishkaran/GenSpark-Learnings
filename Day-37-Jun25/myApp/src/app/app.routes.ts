import { Routes } from '@angular/router';
import { Products } from './products/products';
import { Login } from './login/login';
import { Home } from './home/home';
import { Profile } from './profile/profile';
import { AuthGuard } from './auth-guard';
import { UserList } from './user-list/user-list';
import { FileUploadComponent } from './file-upload/file-upload';

export const routes: Routes = [
    {"path": 'products', component:Products},
    {'path': 'login', component:Login},
    {'path': 'home/:name', component:Home, children: [
        {'path': 'products', component:Products}
    ]},
    {'path': 'profile', component: Profile, canActivate: [AuthGuard]},
    {'path': 'userList', component: FileUploadComponent}
];
