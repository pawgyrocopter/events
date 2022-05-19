import {Component, Input, OnInit} from '@angular/core';
import {Pizza} from "../../_models/pizza";
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  @Input() pizza : Pizza;
  user : User;
  constructor(private accoutnService: AccountService) { }

  ngOnInit(): void {
    this.accoutnService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  addToCart(){
    this.user.cart.pizzas.push(this.pizza);
    console.log(this.user);
  }

}
