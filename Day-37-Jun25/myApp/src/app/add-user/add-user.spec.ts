import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddUser } from './add-user';
import { provideStore } from '@ngrx/store';

describe('AddUser', () => {
  let component: AddUser;
  let fixture: ComponentFixture<AddUser>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddUser],
      providers: [provideStore()]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddUser);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
