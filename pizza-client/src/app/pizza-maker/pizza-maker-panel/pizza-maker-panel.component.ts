import { Component, OnInit } from '@angular/core';
import {Pizza} from "../../_models/pizza";
import {PizzaService} from "../../_services/pizza.service";

@Component({
  selector: 'app-pizza-maker-panel',
  templateUrl: './pizza-maker-panel.component.html',
  styleUrls: ['./pizza-maker-panel.component.css']
})
export class PizzaMakerPanelComponent implements OnInit {
  pizzas : Pizza[];
  constructor(private pizzaService : PizzaService) {
    this.pizzaService.getPizzas().subscribe(response => {
      this.pizzas = response;
    })
  }

  ngOnInit(): void {
  }

}
