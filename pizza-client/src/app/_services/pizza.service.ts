import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {Pizza} from "../_models/pizza";
import {FormGroup} from "@angular/forms";

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

  createPizza(pizzaForm : FormGroup, photoToUpload: File){
    const formFile: FormData = new FormData();
    formFile.append('File', photoToUpload, photoToUpload.name);
    return this.http.post(this.baseUrl +
      'pizzas/add-pizza?name=' + pizzaForm.controls['name'].value +
      '&ingredients=' + pizzaForm.controls['ingredients'].value +
      '&cost=' + pizzaForm.controls['cost'].value +
      '&weight=' + pizzaForm.controls['weight'].value+
      '&photoUrl', formFile);
  }
  updatePizza(pizza : Pizza){
    return this.http.put(this.baseUrl + 'pizzas', pizza);
  }

  updatePizzaState(pizza : Pizza){
    return this.http.put(this.baseUrl + 'pizzas/' + pizza.id +'/' + pizza.state, {});
  }


}
