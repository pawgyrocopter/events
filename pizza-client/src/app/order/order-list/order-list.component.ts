import { Component, OnInit } from '@angular/core';
import {OrderService} from "../../_services/order.service";
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";
import {Order, OrderState} from "../../_models/order";
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-order-list',
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  user : User;
  orders : Order[] = [];
  constructor(private orderService : OrderService, private accountService : AccountService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    this.getOrders();
    const connection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Information)
      .withUrl(environment.hubUrl + 'orders')
      .build();

    connection.start().then(function () {
      console.log('SignalR Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("SendMessage", () => {
      this.getOrders();
    });
  }

  getOrders(){
    this.orderService.getUserOrders(this.user).subscribe(response => {
      this.orders = response
      this.orders.forEach(el => {
        el.orderStateAsString = OrderState[el.orderState];
      })
    })
  }

}
