import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {User} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  createOrder(user : User){
    let order ={
      cart : user.cart,
      name : user.name,
    }
    console.log(order);
    return this.http.post(this.baseUrl + 'order/create-order', order);
  }
}
