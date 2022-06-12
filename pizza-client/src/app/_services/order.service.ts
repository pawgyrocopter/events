import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {User} from "../_models/user";
import {Order} from "../_models/order";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  createOrder(user : User){
    let order ={
      pizzas : user.cart.pizzas,
      name : user.name,
    }
    console.log(order);
    return this.http.post(this.baseUrl + 'order/create-order', order);
  }

  getUserOrders(user : User){
    console.log(this.http.get(this.baseUrl + 'order/get-user-orders/' + user.name));
    return this.http.get(this.baseUrl + 'order/get-user-orders/' + user.name);
  }

  getOrderById(orderId: string | null){
    return this.http.get<Order>(this.baseUrl + 'order/' + orderId);
  }

  getOrders(){
    return this.http.get<Order[]>(this.baseUrl + 'order');
  }

  updateOrderState(orderId : number){
    return this.http.put(this.baseUrl + 'order/' + orderId, {});
  }
}
