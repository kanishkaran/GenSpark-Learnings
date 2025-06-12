import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Async } from './async';

describe('Async', () => {
  let component: Async;
  let fixture: ComponentFixture<Async>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Async]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Async);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
