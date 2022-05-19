import {Component, NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {PizzaListComponent} from "./pizzas/pizza-list/pizza-list.component";
import {PizzaDetailComponent} from "./pizzas/pizza-detail/pizza-detail.component";
import {CartComponent} from "./cart/cart/cart.component";

const routes: Routes = [
  {path: "register", component: RegisterComponent},
  {path: "pizzas", component: PizzaListComponent},
  {path :"pizzas/:name", component: PizzaDetailComponent},
  {path :"cart", component: CartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
