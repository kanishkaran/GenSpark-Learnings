import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { bannedWordsValidator } from '../custom-validations/bannedWordsValidator';
import { passwordStrengthValidator } from '../custom-validations/passwordStrengthValidator';
import { UserService } from '../services/userService';
import { User } from '../models/user';
import { combineLatest, debounceTime, distinctUntilChanged, map, startWith, Subject, tap } from 'rxjs';

@Component({
  selector: 'app-user-management',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './user-management.html',
  styleUrl: './user-management.css'
})
export class UserManagement {
  users: User[] = [];
  userForm: FormGroup;
  bannedWords: string[] = ['admin', 'root'];
  filteredUsers: User[] = [];

  searchString: string = ''
  searchSubject: Subject<string> = new Subject();
  loading: boolean = false;

  selectedRole: string = 'All';
  roleSubject: Subject<string> = new Subject();


  constructor(private userService: UserService) {
    this.userForm = new FormGroup({
      uname: new FormControl(null, [Validators.required, bannedWordsValidator(this.bannedWords)]),
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, [Validators.required, passwordStrengthValidator()]),
      confirmPassword: new FormControl(null, [Validators.required]),
      role: new FormControl(null, Validators.required)
    })

    this.userService.users$.subscribe({
      next: (data) => {
        this.users = data;
        this.filteredUsers = this.users;
        this.setupFiltering();
      }
    })

    this.searchSubject.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe((searchTerm: string) => {
      const lowerSearch = searchTerm.toLowerCase();
      this.filteredUsers = this.users.filter(user =>
        user.username.toLowerCase().includes(lowerSearch) ||
        user.role.toLowerCase().includes(lowerSearch)
      );
    });




  }


  public get username(): any {
    return this.userForm.get('uname');
  }


  public get email(): any {
    return this.userForm.get('email');
  }


  public get password(): any {
    return this.userForm.get('password');
  }


  public get confirmPass(): any {
    return this.userForm.get('confirmPassword');
  }


  public get role(): any {
    return this.userForm.get('role');
  }

  onSubmit() {
    if (this.password.value !== this.confirmPass.value)
      return alert('passwords does not match')

    const { uname, email, password, role } = this.userForm.value;

    const newUser: User = new User(uname, email, password, role);

    this.userService.addUser(newUser);

    this.userService.users$.subscribe({
      next: (data) => {
        this.filteredUsers = data;
      }
    })
  }

  handleSearch() {
    this.searchSubject.next(this.searchString);
  }


  handleRoleFilterChange() {
    this.roleSubject.next(this.selectedRole);
  }

  setupFiltering() {
    const search$ = this.searchSubject.pipe(startWith(this.searchString), debounceTime(300), distinctUntilChanged());
    const role$ = this.roleSubject.pipe(startWith(this.selectedRole));

    combineLatest([search$, role$]).pipe(
      map(([searchTerm, roleFilter]) => {
        const lowerSearch = searchTerm.toLowerCase();
        return this.users.filter(user =>
          (user.username.toLowerCase().includes(lowerSearch) || user.role.toLowerCase().includes(lowerSearch)) &&
          (roleFilter === 'All' || user.role === roleFilter)
        );
      })
    ).subscribe(filtered => {
      this.filteredUsers = filtered;
    });
  }


}
