import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Category } from '../category/category';
import { CategoryService } from '../category/category.service';
import { DishService } from '../dish/dish.service';
import { Dish } from '../dishlist/dish';

@Component({
  selector: 'app-dish-create',
  templateUrl: './dish-create.component.html',
  styleUrls: ['./dish-create.component.css']
})
export class DishCreateComponent implements OnInit {

  dish:Dish = {} as Dish;
  categories:Category[];
  constructor(private dishService:DishService, private categoryService:CategoryService, private location:Location) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe(cat => this.categories = cat);
  }

  saveDish(){
    this.dishService.createDish(this.dish).subscribe(()=>this.back());
  }

  back(){
    this.location.back();
  }


}
