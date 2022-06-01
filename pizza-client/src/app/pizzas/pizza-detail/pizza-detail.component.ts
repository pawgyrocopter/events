import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, ActivatedRouteSnapshot, Router} from "@angular/router";
import {take} from "rxjs";
import {PizzaService} from "../../_services/pizza.service";
import {Pizza} from "../../_models/pizza";
import {TopingService} from "../../_services/toping.service";
import {Toping} from "../../_models/toping";
import {AccountService} from "../../_services/account.service";
import {User} from "../../_models/user";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-pizza-detail',
  templateUrl: './pizza-detail.component.html',
  styleUrls: ['./pizza-detail.component.css']
})
export class PizzaDetailComponent implements OnInit {
  name: string | null = "";
  pizza : Pizza | undefined;
  topings : Toping[] = [];
  user : User;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private pizzasService: PizzaService,
              private topingService : TopingService,
              private accountService: AccountService,
              private toastr: ToastrService) {
    this.accountService.currentUser$.subscribe(user => {
      this.user = user;
    })
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(response => {
      this.name = response.get('name');
    })
    this.pizzasService.getPizzaByName(this.name).subscribe( response =>{
      this.pizza = response;
    })
    this.topingService.getTopings().subscribe(response => {
      this.topings = response;
      this.topings.forEach(toping => {
        toping.counter = 0;
      })
      console.log(this.topings);
    })
  }

  increaseCounter(id : number){
    this.topings[id-1].counter = this.topings[id-1].counter + 1 ;
    if(this.pizza){
      this.pizza.cost = this.pizza.cost + 20;
    }
    console.log(this.topings);
  }
  decreaseCounter(id : number){
    this.topings[id-1].counter = this.topings[id-1].counter - 1 ;
    if(this.pizza){
      this.pizza.cost = this.pizza.cost - 20;
    }
    console.log(this.topings);
  }

  addToCart(){
    // @ts-ignore
    this.pizza?.topings = this.topings;
    if (this.pizza) {
      this.user.cart.pizzas.push(this.pizza);
    }
    this.toastr.success("Well done, you successfully added pizza to your cart");
    this.router.navigateByUrl('/pizzas');
    console.log(this.user);
  }

}
