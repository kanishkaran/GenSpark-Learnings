import { BehaviorSubject, Observable } from "rxjs";
import { UserModel } from "../models/UserModel";
import { UserLoginModel } from "../models/userLoginModel";
import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class LoginService{
    http = inject(HttpClient)

    private usernameSubject = new BehaviorSubject<string | null>(null);
    username$: Observable<string | null> = this.usernameSubject.asObservable();
    // dummyData : UserLoginModel[] = [
    //     {username: "henry@gmail.com", password: "henry123"},
    //     {username: "john@gmail.com", password: "john123"}

    // ]

    validateUser(user: UserLoginModel){
        // return this.dummyData.some(
        //     u => u.username == user.username && u.password == user.password
        // )
    }

    validateUserOb(user: UserLoginModel){
        // var result = this.dummyData.some(
        //     u => u.username == user.username && u.password == user.password
        // )

        if(user.username.length > 3){
            this.callLoginAPI(user).subscribe({
                next: (data : any) => {
                    this.usernameSubject.next(user.username);
                    localStorage.setItem('token', data.accessToken);

                }
            })
            
            
        }
        else{
            this.usernameSubject.error("Invalid Credentials")
        }
    }

    callLoginAPI(user: UserLoginModel){
        return this.http.post("https://dummyjson.com/auth/login",user);
    }

    getProfile(){
         var token = localStorage.getItem("token")
        const httpHeader = new HttpHeaders({
            'Authorization':`Bearer ${token}`
        })
        return this.http.get('https://dummyjson.com/auth/me',{headers:httpHeader});
    }
}