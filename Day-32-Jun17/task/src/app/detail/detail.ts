import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-detail',
  imports: [],
  templateUrl: './detail.html',
  styleUrl: './detail.css'
})
export class Detail {
errorMessage: string = ''
router = inject(ActivatedRoute)

  ngOnInit(){
    this.errorMessage = this.router.snapshot.params['msg'] as string
  }

}
