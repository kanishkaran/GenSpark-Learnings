import { Component, inject, Injectable } from '@angular/core';
import { LoginService } from '../services/loginService';
import { UserModel } from '../models/UserModel';

@Component({
  selector: 'app-profile',
  imports: [],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})

@Injectable()
export class Profile {
  loginService = inject(LoginService);
  user: UserModel = new UserModel();

  constructor(){
        this.loginService.getProfile().subscribe({
          next:(data:any)=>{
            this.user = UserModel.fromForm(data);
          }
        })
     }
}
