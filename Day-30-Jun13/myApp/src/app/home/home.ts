import { Component } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { LoginService } from '../services/loginService';


@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home {
  user: UserModel | undefined;
  username: string | null = null;

  ngOnInit(){
    const stored = localStorage.getItem('user');

    this.user = stored ? JSON.parse(stored) : null;
  }

  constructor(private loginService: LoginService){
    this.loginService.username$.subscribe({
      next: (data) => {
        this.username = data;
      },
      error: (data) => {
        alert(data)
      }
    })
  }

}
