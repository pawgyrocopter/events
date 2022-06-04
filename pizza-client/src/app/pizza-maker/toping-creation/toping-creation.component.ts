import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PizzaService} from "../../_services/pizza.service";
import {Router} from "@angular/router";
import {TopingService} from "../../_services/toping.service";

@Component({
  selector: 'app-toping-creation',
  templateUrl: './toping-creation.component.html',
  styleUrls: ['./toping-creation.component.css']
})
export class TopingCreationComponent implements OnInit {
  creationForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private topingService: TopingService,
              private fb: FormBuilder,
              private router: Router) {
    this.creationForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.creationForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  createToping() {
    this.topingService.createToping(this.creationForm.controls['name'].value).subscribe(response =>{
      console.log(response);
    }, error => console.log(error));
  }
}
