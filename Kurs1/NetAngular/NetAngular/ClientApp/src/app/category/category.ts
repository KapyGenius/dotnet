import { Dish } from "../dishlist/dish";

export interface Category {
    id:number;
    name:string;
    description:string;
    dishes: Dish[];
}
