import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from './category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private https:HttpClient) { }

  getCategories():Observable<Category[]>{
    return this.https.get<Category[]>("api/category")
  }
  getCategoryByIdid(id:number):Observable<Category>{
    return this.https.get<Category>(`api/category/${id}`)
  }
}
