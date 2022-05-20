import {Component, NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {PizzaListComponent} from "./pizzas/pizza-list/pizza-list.component";
import {PizzaDetailComponent} from "./pizzas/pizza-detail/pizza-detail.component";
import {CartComponent} from "./cart/cart/cart.component";
import {PizzaMakerPanelComponent} from "./pizza-maker/pizza-maker-panel/pizza-maker-panel.component";
import {AuthGuard} from "./_guards/auth.guard";

const routes: Routes = [
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {path: "pizzas", component: PizzaListComponent},
      {path: "pizzas/:name", component: PizzaDetailComponent},
      {path: "cart", component: CartComponent},
      {path: "orders", component: PizzaMakerPanelComponent}
    ]
  },
  {path: "register", component: RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
