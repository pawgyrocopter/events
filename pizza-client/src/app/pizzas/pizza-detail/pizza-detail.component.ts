import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, ActivatedRouteSnapshot, Router} from "@angular/router";
import {take} from "rxjs";
import {PizzaService} from "../../_services/pizza.service";
import {Pizza} from "../../_models/pizza";

@Component({
  selector: 'app-pizza-detail',
  templateUrl: './pizza-detail.component.html',
  styleUrls: ['./pizza-detail.component.css']
})
export class PizzaDetailComponent implements OnInit {
  name: string | null = "";
  pizza : Pizza | undefined;
  constructor(private router: Router, private route: ActivatedRoute, private pizzasService: PizzaService) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(response => {
      this.name = response.get('name');
    })
    this.pizzasService.getPizzaByName(this.name).subscribe( response =>{
      this.pizza = response;
    })
  }

}
