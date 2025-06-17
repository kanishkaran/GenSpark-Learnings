import { Component } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { LoginService } from '../services/loginService';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginModel } from '../models/userLoginModel';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
user: UserLoginModel = new UserLoginModel();
  constructor(private loginService: LoginService, private route: Router){

  }

  // handleLogin(username: string, password: string){
  //   let user = new UserLoginModel(username, password);
  //   let result = this.loginService.validateUser(user)
  //   if(result){
  //     localStorage.setItem('user',JSON.stringify(user));
  //     this.route.navigate(['home', user.username])
  //   }
  //   else{
  //     alert("Login Failed")
  //   }
  // }

  handleLoginObservable(){
    this.loginService.validateUserOb(this.user);
    this.route.navigateByUrl('/home/' + this.user.username)
  }
}
