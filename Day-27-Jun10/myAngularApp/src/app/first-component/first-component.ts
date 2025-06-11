import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-first-component',
  imports: [FormsModule],
  templateUrl: './first-component.html',
  styleUrl: './first-component.css'
})
export class FirstComponent {
  name: string;
  className: string = "bi bi-balloon-heart";
  like: boolean = false;
  constructor() {
    this.name = "albert";
  }

  onButtonClick(name:string){
    this.name = name;
  }

  toggleLike(){
    this.like = !this.like
    if(this.like)
      this.className = "bi bi-balloon-heart-fill";
    else
      this.className = "bi bi-balloon-heart";
  }

}
