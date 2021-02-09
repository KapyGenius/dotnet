import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Category } from '../category/category';
import {CategoryService} from "../category/category.service"

@Component({
  selector: 'app-dishlist',
  templateUrl: './dishlist.component.html',
  styleUrls: ['./dishlist.component.css']
})
export class DishlistComponent implements OnInit {
  
  category:Category;
  
  constructor(private route:ActivatedRoute, private categoryService:CategoryService) { }

  ngOnInit() {
    const categoryId:number = parseInt(this.route.snapshot.paramMap.get('categoryId'));
    this.categoryService.getCategoryByIdid(categoryId).subscribe(cat => this.category = cat);
  
  }

}
