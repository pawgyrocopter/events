import {Component, Input, OnInit} from '@angular/core';
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  user : User;
  topings : string;
  constructor(private accountService: AccountService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
  }


}
