import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Route, Router} from "@angular/router";
import {PizzaService} from "../../_services/pizza.service";
import {TopingService} from "../../_services/toping.service";
import {AccountService} from "../../_services/account.service";
import {ToastrService} from "ngx-toastr";
import { Order } from 'src/app/_models/order';
import {OrderService} from "../../_services/order.service";

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.css']
})
export class OrderDetailComponent implements OnInit {
  order : Order |undefined;
  id : string | null;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private pizzasService: PizzaService,
              private topingService : TopingService,
              private accountService: AccountService,
              private toastr: ToastrService,
              private orderService: OrderService) {
    this.route.paramMap.subscribe(response => {
      this.id = response.get('id');
    })
    this.orderService.getOrderById(this.id).subscribe(response => {
      this.order = response;
    })
  }

  ngOnInit(): void {

  }

}
