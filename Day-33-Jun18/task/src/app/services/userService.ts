import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserModel } from "../models/userModel";

@Injectable()
export class UserService{
    http = inject(HttpClient);
    userSubject = new BehaviorSubject<any | null>(null);
    user$ = this.userSubject.asObservable();

    getAllUsers(){
        return this.http.get('https://dummyjson.com/users');
    }

    addUser(user: any){
        return this.http.post('https://dummyjson.com/users/add', user);
    }
}