import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { UserModel } from '../models/UserModel';
import { addUser } from '../ngrx/users.action';

@Component({
  selector: 'app-add-user',
  imports: [],
  templateUrl: './add-user.html',
  styleUrl: './add-user.css'
})
export class AddUser {
  constructor(private store: Store) {

  }
  handelAddUser() {
    const newUser = new UserModel(101, "TestName", "example@gmail.com", "", "", "M", "");
    this.store.dispatch(addUser({ user: newUser }));
  }
}
