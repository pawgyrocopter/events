import { Component, OnInit } from '@angular/core';
import {OrderService} from "../../_services/order.service";
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";

@Component({
  selector: 'app-pizza-user-orders',
  templateUrl: './pizza-user-orders.component.html',
  styleUrls: ['./pizza-user-orders.component.css']
})
export class PizzaUserOrdersComponent implements OnInit {
  orders : any;
  user : User;
  constructor(private orderService: OrderService, private accountService: AccountService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    this.orderService.getUserOrders(this.user).subscribe( response => {
      this.orders = response;
      console.log(response);
    })
  }

}
