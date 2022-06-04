import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {PizzaService} from "../../_services/pizza.service";
import {TopingService} from "../../_services/toping.service";
import {AccountService} from "../../_services/account.service";
import {ToastrService} from "ngx-toastr";
import {Pizza} from "../../_models/pizza";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-pizza-edit',
  templateUrl: './pizza-edit.component.html',
  styleUrls: ['./pizza-edit.component.css']
})
export class PizzaEditComponent implements OnInit {
  name : string | null = "";
  pizza : Pizza | undefined;
  editingForm: FormGroup;
  constructor(private router: Router,
              private route: ActivatedRoute,
              private pizzasService: PizzaService,
              private topingService : TopingService,
              private accountService: AccountService,
              private toastr: ToastrService,
              private fb: FormBuilder) {
    this.editingForm = this.fb.group({
      ingredients: ['', Validators.required],
      cost: ['', Validators.required],
      weight: ['', Validators.required],
      photo: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(response => {
      this.name = response.get('name');
    })
    this.pizzasService.getPizzaByName(this.name).subscribe( response =>{
      this.pizza = response;
    })
    this.editingForm = this.fb.group({
      ingredients: ['', Validators.required],
      cost: ['', Validators.required],
      weight: ['', Validators.required],
      photo: ['', Validators.required],
    });
  }

  updatePizza() {
    if (this.pizza) {
      this.pizza.cost = this.editingForm.controls['cost'].value;
      this.pizza.ingredients = this.editingForm.controls['ingredients'].value;
      this.pizza.weight = this.editingForm.controls['weight'].value;
      console.log(this.pizza);
      this.pizzasService.updatePizza(this.pizza).subscribe(response => {
        console.log(response);
      },error => console.log(error));
    }
  }
}
