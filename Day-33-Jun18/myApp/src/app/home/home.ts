import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterOutlet],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class Home implements OnInit{
  userName: string = "";
  router = inject(ActivatedRoute)
  
  ngOnInit(){
    this.userName = this.router.snapshot.params['name'] as string;
  }

}
