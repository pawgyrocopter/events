import {Component, Input, OnInit} from '@angular/core';
import {Order} from "../../_models/order";

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.css']
})
export class OrderItemComponent implements OnInit {
  @Input() order: Order;
  image: string = 'assets/';

  constructor() {
  }

  ngOnInit(): void {
    this.image += this.changeImage();
  }

  changeImage() {
    return this.order.orderState === 1 ? 'ok.jpg' : 'making.jpg';
  }

}
