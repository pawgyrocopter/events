import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {map, ReplaySubject} from "rxjs";
import {User} from "../_models/user";

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) { }

  login(model : any){
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map( (response : any) => {
        const user = response;
        console.log(response);
        if(user){
          this.setCurrentUser(user);
        }
      })
    );
  }
  setCurrentUser(user: User) {
    user.roles = [];
    user.cart = {pizzas: []};
    const roles = this.getDecodedToken(user.token).role;
    Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
  getDecodedToken(token : string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

}
