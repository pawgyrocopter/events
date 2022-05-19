import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import {Pizza} from "../../_models/pizza";
import {PizzaService} from "../../_services/pizza.service";

@Component({
  selector: 'app-pizza-list',
  templateUrl: './pizza-list.component.html',
  styleUrls: ['./pizza-list.component.css']
})
export class PizzaListComponent implements OnInit {
  pizzas :Pizza[] = [];
  constructor(private pizzaService:PizzaService) { }

  ngOnInit(): void {
    this.loadPizzas();
  }

  loadPizzas(){
    this.pizzaService.getPizzas().subscribe(response  => {
      this.pizzas = response;
    });

  }

}
