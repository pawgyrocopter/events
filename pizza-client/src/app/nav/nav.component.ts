import { Component, OnInit } from '@angular/core';
import {AccountService} from "../_services/account.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public accountService : AccountService, private router: Router) { }

  ngOnInit(): void {
  }

  login(): void {
    this.accountService.login(this.model).subscribe(response => {
        this.router.navigateByUrl("/pizzas");
      },
      error => {
        console.log(error);
      });
  }

  logout() {
    this.router.navigateByUrl("/");
  }

}
