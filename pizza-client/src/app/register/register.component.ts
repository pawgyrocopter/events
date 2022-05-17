  import {Component, EventEmitter, OnInit, Output} from '@angular/core';
  import {AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators} from "@angular/forms";
  import {Router} from "@angular/router";
  import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  registerForm: FormGroup;
  validationErrors: string[] = [];

  constructor(private fb: FormBuilder, private router: Router, private http : HttpClient) {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['',Validators.required],
      adress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(8)
        ]],
    });
  }

  ngOnInit(): void {
    this.initializeForm()
  }

  register() {
    console.log(this.registerForm.value);
    let user = {
      name : this.registerForm.value.name,
      adress : this.registerForm.value.adress,
      password : this.registerForm.value.password,
      phoneNumber : this.registerForm.value.phoneNumber,
      email : this.registerForm.value.email,
    }
    console.log(user);
    this.http.post('https://localhost:7222/api/account/register', user).subscribe(user => {
      console.log(user);
    },error => console.log(error));
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['',Validators.required],
      adress: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      password: ['',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(8)
        ]],
    })
  }

}
