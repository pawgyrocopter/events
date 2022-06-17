import { Component, OnInit } from '@angular/core';
import {Pizza} from "../../_models/pizza";
import {PizzaService} from "../../_services/pizza.service";
import {OrderService} from "../../_services/order.service";
import {Order} from "../../_models/order";
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-pizza-maker-panel',
  templateUrl: './pizza-maker-panel.component.html',
  styleUrls: ['./pizza-maker-panel.component.css']
})
export class PizzaMakerPanelComponent implements OnInit {
  pizzas : Pizza[] =[];
  orders : Order[] =[];
  constructor(private pizzaService : PizzaService, private orderService : OrderService) {
  }

  ngOnInit(): void {
    this.getPizzas();
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

  getPizzas(){
    this.pizzaService.getPizzas().subscribe(response => {
      this.pizzas = response;
    })
  }
  getOrders(){
    this.orderService.getOrders().subscribe(response => {
      this.orders = response;
    })
  }

}
