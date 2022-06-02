import {Component, NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {RegisterComponent} from "./register/register.component";
import {PizzaListComponent} from "./pizzas/pizza-list/pizza-list.component";
import {PizzaDetailComponent} from "./pizzas/pizza-detail/pizza-detail.component";
import {CartComponent} from "./cart/cart/cart.component";
import {AuthGuard} from "./_guards/auth.guard";
import {PizzaUserOrdersComponent} from "./pizzas/pizza-user-orders/pizza-user-orders.component";
import {OrderDetailComponent} from "./order/order-detail/order-detail.component";
import {PizzaMakerPanelComponent} from "./pizza-maker/pizza-maker-panel/pizza-maker-panel.component";

const routes: Routes = [
  {
    path: "",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    children: [
      {path: "pizzas", component: PizzaListComponent},
      {path: "pizzas/:name", component: PizzaDetailComponent},
      {path: "cart", component: CartComponent},
      {path: "orders", component: PizzaUserOrdersComponent},
      {path: "orders/:id", component: OrderDetailComponent},
    ]
  },
  {path: "pizza-maker-panel", component: PizzaMakerPanelComponent},
  {path: "register", component: RegisterComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
