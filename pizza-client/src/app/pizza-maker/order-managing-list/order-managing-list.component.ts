import {Component, Input, OnInit} from '@angular/core';
import {Order} from "../../_models/order";

@Component({
  selector: 'app-order-managing-list',
  templateUrl: './order-managing-list.component.html',
  styleUrls: ['./order-managing-list.component.css']
})
export class OrderManagingListComponent implements OnInit {
  @Input() orders : Order[];
  constructor() { }

  ngOnInit(): void {
  }

}
