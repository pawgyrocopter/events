import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";
import {OrderService} from "../../_services/order.service";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  user : User;
  topings : string;
  constructor(private accountService: AccountService, private orderService : OrderService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
  }

  orderCreate(user : User) {
    this.orderService.createOrder(user).subscribe(response => {
      console.log(response);
    })
  }
}
