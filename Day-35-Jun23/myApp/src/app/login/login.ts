import { Component } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { LoginService } from '../services/loginService';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserLoginModel } from '../models/userLoginModel';
import { textValidator } from '../misc/textValidator';

@Component({
  selector: 'app-login',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  user: UserLoginModel = new UserLoginModel();
  loginForm: FormGroup;

  constructor(private loginService: LoginService, private route: Router) {
    this.loginForm = new FormGroup({
      uname: new FormControl(null, Validators.required),
      pass: new FormControl(null, [Validators.required, textValidator()])
    })
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

   
  public get username(): any {
    return this.loginForm.get('uname');
  }

  
  public get password() : any{
    return this.loginForm.get('pass')
  }
  
  

  handleLoginObservable() {

    // if(username.control.errors || password.control.errors)
    //   return;
    this.loginService.validateUserOb(this.user);
    this.route.navigateByUrl('/home/' + this.user.username)
  }
}
