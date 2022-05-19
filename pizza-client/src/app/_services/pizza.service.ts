import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Pizza} from "../_models/pizza";

@Injectable({
  providedIn: 'root'
})
export class PizzaService {
  baseUrl = environment.apiUrl;
  constructor(private http : HttpClient) { }

  getPizzas(){
   return this.http.get<Pizza[]>(this.baseUrl + 'pizzas');
  }

  getPizzaByName(name : string | null){
    return this.http.get<Pizza>(this.baseUrl + 'pizzas/' + name);
  }
}
