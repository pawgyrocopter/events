import { Component, OnInit } from '@angular/core';
import {Pizza} from "../../_models/pizza";
import {PizzaService} from "../../_services/pizza.service";
import {OrderService} from "../../_services/order.service";
import {Order} from "../../_models/order";

@Component({
  selector: 'app-pizza-maker-panel',
  templateUrl: './pizza-maker-panel.component.html',
  styleUrls: ['./pizza-maker-panel.component.css']
})
export class PizzaMakerPanelComponent implements OnInit {
  pizzas : Pizza[] =[];
  orders : Order[] =[];
  constructor(private pizzaService : PizzaService, private orderService : OrderService) {
    this.pizzaService.getPizzas().subscribe(response => {
      this.pizzas = response;
    })
    this.orderService.getOrders().subscribe(response => {
      this.orders = response;
    })
  }

  ngOnInit(): void {
  }

}
