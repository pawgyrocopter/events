import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import {NgbDropdown, NgbDropdownModule, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from './register/register.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { PizzaListComponent } from './pizzas/pizza-list/pizza-list.component';
import { PizzaItemComponent } from './pizzas/pizza-item/pizza-item.component';
import { PizzaDetailComponent } from './pizzas/pizza-detail/pizza-detail.component';
import { CartComponent } from './cart/cart/cart.component';
import { PizzaUserOrdersComponent } from './pizzas/pizza-user-orders/pizza-user-orders.component';
import { OrderItemComponent } from './order/order-item/order-item.component';
import { OrderListComponent } from './order/order-list/order-list.component';
import {ToastrModule, ToastrService} from "ngx-toastr";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {NgxSpinnerModule} from "ngx-spinner";
import {AccordionModule} from "ngx-bootstrap/accordion";
import { ModalModule } from 'ngx-bootstrap/modal';
import { OrderDetailComponent } from './order/order-detail/order-detail.component';
import { PizzaMakerPanelComponent } from './pizza-maker/pizza-maker-panel/pizza-maker-panel.component';
import {TabsModule} from "ngx-bootstrap/tabs";
import { PizzaCreationComponent } from './pizza-maker/pizza-creation/pizza-creation.component';
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { TopingCreationComponent } from './pizza-maker/toping-creation/toping-creation.component';
import { PizzaEditComponent } from './pizza-maker/pizza-edit/pizza-edit.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { UserManagmentComponent } from './admin/user-managment/user-managment.component';
import { RolesModalComponent } from './modals/roles-modal/roles-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    TextInputComponent,
    PizzaListComponent,
    PizzaItemComponent,
    PizzaDetailComponent,
    CartComponent,
    PizzaUserOrdersComponent,
    OrderItemComponent,
    OrderListComponent,
    OrderDetailComponent,
    PizzaMakerPanelComponent,
    PizzaCreationComponent,
    TopingCreationComponent,
    PizzaEditComponent,
    AdminPanelComponent,
    UserManagmentComponent,
    RolesModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbDropdownModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    NgxSpinnerModule,
    AccordionModule,
    ModalModule.forRoot(),
    TabsModule,
    BsDropdownModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
