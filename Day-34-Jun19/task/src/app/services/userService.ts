import { BehaviorSubject, Observable } from "rxjs";
import { User } from "../models/user";


export class UserService{
    private usersSubject : BehaviorSubject<User[]> = new BehaviorSubject<User[]>([
        new User('Mark', 'mark@gmail.com', 'mark123', 'user'),
        new User('Antory', 'at@gmail.com', 'at123', 'user'),
        new User('Bobby', 'bob@gmail.com', 'bob123', 'admin')
    ])

    users$ : Observable<User[]> = this.usersSubject.asObservable();

    addUser(user: User){
        const current = this.usersSubject.value;
        this.usersSubject.value.push(user);
    }
}