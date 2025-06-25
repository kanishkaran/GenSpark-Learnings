import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Recipe } from './recipe';
import { recipeModel } from '../../models/recipe.model';
import { Component } from '@angular/core';


@Component({
  standalone: true,
  imports: [Recipe],
  template: `<app-recipe [recipe] ="recipeModel" ></app-recipe>`
})
class ParentComponent{
  recipes: recipeModel = new recipeModel();

}

describe('Recipe', () => {
  let component: Recipe
  let fixture: ComponentFixture<ParentComponent>;
  let childFixture: ComponentFixture<Recipe>;
  let parentComponent : ParentComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Recipe]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParentComponent);
    parentComponent = fixture.componentInstance;
    fixture.detectChanges();

    childFixture = TestBed.createComponent(Recipe);
    component = childFixture.componentInstance;
    childFixture.detectChanges();

  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should render component', () => {
    parentComponent.recipes = {
      id : 1,
      name: "Some-food",
      prepTimeMinutes: 20,
      cuisine: "Cuisine",
      cookTimeMinutes: 10
    } as recipeModel;

    fixture.detectChanges();
    const content = fixture.nativeElement as HTMLElement;
    expect(content.textContent).toContain("Cuisine")
  })
});
