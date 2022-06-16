import {Component, OnInit} from '@angular/core';
import {PizzaService} from "../../_services/pizza.service";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-pizza-creation',
  templateUrl: './pizza-creation.component.html',
  styleUrls: ['./pizza-creation.component.css']
})
export class PizzaCreationComponent implements OnInit {
  creationForm: FormGroup;
  validationErrors: string[] = [];
  fileToUpload: File | null = null;

  constructor(private pizzaService: PizzaService,
              private fb: FormBuilder,
              private router: Router,
              private toast: ToastrService) {
    this.creationForm = this.fb.group({
      name: ['', Validators.required],
      ingredients: ['', Validators.required],
      cost: ['', Validators.required],
      weight: ['', Validators.required],
      photo: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.creationForm = this.fb.group({
      name: ['', Validators.required],
      ingredients: ['', Validators.required],
      cost: ['', Validators.required],
      weight: ['', Validators.required],
      photo: ['', Validators.required],
    });
  }

  createPizza() {
    console.log(this.creationForm.value);
    this.uploadFileToActivity();
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFileToActivity() {
    if (this.fileToUpload) {
      this.pizzaService.createPizza(this.creationForm, this.fileToUpload)
        .subscribe(data => {
          console.log(data);
          this.toast.success('Pizza was successfully added!');
          this.router.navigateByUrl('/pizza-maker-panel');
        });
    }
  }
}
