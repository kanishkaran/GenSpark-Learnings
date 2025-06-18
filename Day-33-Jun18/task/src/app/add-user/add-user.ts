import { Component } from '@angular/core';
import { UserModel } from '../models/userModel';
import { UserService } from '../services/userService';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-user',
  imports: [FormsModule],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css'
})
export class AddUser {
  user: UserModel = new UserModel();

  constructor(private userService: UserService, private router: Router){}

  addUser(){
    this.userService.addUser(this.user).subscribe({
      next: (data) => {
        alert('User Added');
        this.userService.userSubject.next(data)
        this.router.navigate(['/users'])
      }
    })
  }

}
