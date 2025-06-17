import { HttpClient } from "@angular/common/http";
import { UserLoginModel } from "../models/userLoginModel";
import { inject, Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class LoginService{
    http = inject(HttpClient)
    errorString : Subject<string> = new Subject<string>();
    error$ = this.errorString.asObservable();

    validateUser(user: UserLoginModel){

        if(user.username.length > 3){
            this.callLoginAPI(user).subscribe({
                next: (data : any) => {
                    localStorage.setItem('token', data.accessToken)
                },
                error: (err) => {
                    this.errorString.next(err)
                }
            })
        }
    }

    callLoginAPI(user : UserLoginModel){
        return this.http.post('https://dummyjson.com/auth/login', user);
    }
}