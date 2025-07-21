import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-nav',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {

}
