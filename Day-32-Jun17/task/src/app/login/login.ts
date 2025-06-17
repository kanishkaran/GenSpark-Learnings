import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserLoginModel } from '../models/userLoginModel';
import { LoginService } from '../services/loginService';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
user: UserLoginModel = new UserLoginModel();
loginService = inject(LoginService);


handleLogin() {
this.loginService.validateUser(this.user);
}

}
