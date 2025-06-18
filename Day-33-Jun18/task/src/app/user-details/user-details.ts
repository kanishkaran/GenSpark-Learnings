import { Component } from '@angular/core';
import { UserService } from '../services/userService';
import { FormsModule } from '@angular/forms';
import { UserModel } from '../models/userModel';

@Component({
  selector: 'app-user-details',
  imports: [FormsModule],
  templateUrl: './user-details.html',
  styleUrl: './user-details.css'
})
export class UserDetails {
  users: UserModel[] = [];
  filteredUsers: UserModel[] = [];
  gender: string = '';
  role: string = '';
  state: string = '';

  constructor(private userService: UserService) {

  }

  ngOnInit() {
    this.userService.getAllUsers().subscribe({
      next: (data: any) => {
        this.users = data.users as UserModel[];
        this.filteredUsers = this.users as UserModel[];
      },
      error: (err) => {
        alert("Error while fetching data")
      }
    });

    this.userService.user$.subscribe({
      next: (data: any) => {
        if (!data) return;

        this.users = [...this.users, data];
        this.filteredUsers = this.users;

      }
    })
  }

  handleFilters() {
    this.filteredUsers = this.users.filter(user =>
      (!this.gender || user.gender === this.gender) &&
      (!this.role || user.role === this.role) &&
      (!this.state || user.address?.state === this.state)
    )
  }
}
