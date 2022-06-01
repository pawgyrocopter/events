import {Component, Input, OnInit, TemplateRef} from '@angular/core';
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";
import {OrderService} from "../../_services/order.service";
import {Toping} from "../../_models/toping";
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {Pizza} from "../../_models/pizza";
import {ToastrService} from "ngx-toastr";
import {cloneDeep} from 'lodash';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  user : User;
  modalRef: BsModalRef;
  pizza : Pizza;
  pizzaCopy : Pizza;
  constructor(private accountService: AccountService,
              private orderService : OrderService,
              private modalService: BsModalService,
              private toast:ToastrService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {}


  orderCreate(user : User) {
    console.log(user.cart);
    this.orderService.createOrder(user).subscribe(response => {
      console.log(response);
      this.user.cart.pizzas = [];
      this.toast.success("You successfully created order");
    })
  }

  addTopings(pizza : Pizza, template: TemplateRef<any>){
    this.pizza = pizza;
    this.pizzaCopy = cloneDeep(pizza);
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  deletePizza(pizza : Pizza, template: TemplateRef<any>) {
    this.pizza = pizza;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirmDeletion() {
    const index = this.user.cart.pizzas.indexOf(this.pizza, 0);
    if (index > -1) {
      this.user.cart.pizzas.splice(index, 1);
    }
    this.modalRef.hide();
  }

  declineDeletion() {
    this.modalRef.hide();
  }

  minusToping(toping: Toping) {
    if(toping.counter > 0){
      toping.counter = toping.counter -1;
      this.pizzaCopy.cost -= 20;
    }
  }

  plusToping(toping: Toping) {
    toping.counter = toping.counter + 1;
    this.pizzaCopy.cost += 20;
  }

  confirmAddTopings() {
    const index = this.user.cart.pizzas.indexOf(this.pizza, 0);
    this.user.cart.pizzas[index] = this.pizzaCopy;
    this.modalRef.hide();
  }

  canselAddTopings() {
    this.modalRef.hide();
  }
}
