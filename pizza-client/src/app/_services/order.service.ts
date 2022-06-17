import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {User} from "../_models/user";
import {Order} from "../_models/order";
import {HubConnection} from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  constructor(private http:HttpClient) { }

  createOrder(user : User){
    let order ={
      pizzas : user.cart.pizzas,
      name : user.name,
    }
    console.log(order);
    //return this.hubConnection.invoke('SendMessage', order.name, '123');
    return this.http.post(this.baseUrl + 'order/create-order', order);
  }

  getUserOrders(user : User){
    console.log(this.http.get(this.baseUrl + 'order/get-user-orders/' + user.name));
    return this.http.get<Order[]>(this.baseUrl + 'order/get-user-orders/' + user.name);
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
