import { Component } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { LoginService } from '../services/loginService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
user: UserModel = new UserModel();
  constructor(private loginService: LoginService){

  }

  handleLogin(username: string, password: string){
    let user = new UserModel(username, password);
    let result = this.loginService.validateUser(user)
    if(result){
      localStorage.setItem('user',JSON.stringify(user));
      alert("Login Success");
    }
    else{
      alert("Login Failed")
    }
  }

  handleLoginObservable(){
    this.loginService.validateUserOb(this.user)
  }
}
