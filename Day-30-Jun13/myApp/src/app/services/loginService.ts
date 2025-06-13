import { BehaviorSubject, Observable } from "rxjs";
import { UserModel } from "../models/UserModel";

export class LoginService{

    private usernameSubject = new BehaviorSubject<string | null>(null);
    username$: Observable<string | null> = this.usernameSubject.asObservable();
    dummyData : UserModel[] = [
        {username: "henry@gmail.com", password: "henry123"},
        {username: "john@gmail.com", password: "john123"}
    ]

    validateUser(user: UserModel){
        return this.dummyData.some(
            u => u.username == user.username && u.password == user.password
        )
    }

    validateUserOb(user: UserModel){
        var result = this.dummyData.some(
            u => u.username == user.username && u.password == user.password
        )

        if(result){
            this.usernameSubject.next(user.username);
        }
        else{
            this.usernameSubject.error("Invalid Credentials")
        }
    }
}