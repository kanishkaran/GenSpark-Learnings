import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { Recipes } from './recipes';
import { RecipeService } from '../../service/recipe-service';
import { of } from 'rxjs';
import { recipeModel } from '../../models/recipe.model';
import { Recipe } from '../recipe/recipe';
import { FormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('Recipes Component', () => {
  let component: Recipes;
  let fixture: ComponentFixture<Recipes>;
  let mockRecipeService: jasmine.SpyObj<RecipeService>;

  const mockRecipes: any = {
    recipes: [
      { id: 1, name: 'Pasta' },
      { id: 2, name: 'Pizza' }
    ]
  }

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('RecipeService', ['getSearchResult', 'getAllRecipes']);

    await TestBed.configureTestingModule({
      imports: [Recipes],
      providers: [{ provide: RecipeService, useValue: spy }],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();

    fixture = TestBed.createComponent(Recipes);
    component = fixture.componentInstance;
    mockRecipeService = TestBed.inject(RecipeService) as jasmine.SpyObj<RecipeService>;
    mockRecipeService.getSearchResult.and.returnValue(of(mockRecipes));
    mockRecipeService.getAllRecipes.and.returnValue(of(mockRecipes))
    fixture.detectChanges()
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should debounce and search ', fakeAsync ( () => {
    component.searchTerm = 'Pasta';
    component.handleSearch();

    tick(400);
    fixture.detectChanges();

    expect(mockRecipeService.getSearchResult).toHaveBeenCalledWith('Pasta')
  }))

  it('should render all recipes', () => {
   
    expect(mockRecipeService.getAllRecipes).toHaveBeenCalled();
    expect(component.recipes).toEqual(mockRecipes.recipes)
  })



});
