import { Component } from '@angular/core';
import { AsyncService } from '../services/AsyncService';
import { UserModel } from '../models/userModel';



@Component({
  selector: 'app-async',
  imports: [],
  templateUrl: './async.html',
  styleUrl: './async.css'
})
export class Async {
  userData: UserModel[] | undefined = undefined;
  className: string = "hide";
  constructor(private asyncService: AsyncService) {

  }

  displayData(){
    this.asyncService.getUsersCb((data) => {
      this.userData = data;
      this.className = "show";
    })
  }

  displayDataPromise(){
    this.asyncService.getUsersPromise().then((data) => {
      this.userData = data;
      this.className = "show";
    })
    .catch((err) => {
      console.log(err);
    })
  }

  async displayDataAsync(){
    this.userData = await this.asyncService.getUsersPromise();
    this.className = "show";
  }

}
