import {Component, Input, OnInit} from '@angular/core';
import {PizzaService} from "../../_services/pizza.service";
import { Pizza} from "../../_models/pizza";
import {AccountService} from "../../_services/account.service";
import {async, take} from "rxjs";

@Component({
  selector: 'app-pizza-item',
  templateUrl: './pizza-item.component.html',
  styleUrls: ['./pizza-item.component.css']
})
export class PizzaItemComponent implements OnInit {
  @Input() pizza!: Pizza;

  constructor(private memberSerivice: PizzaService,
              public accountService : AccountService) {
  }

  ngOnInit(): void {
  }

}
