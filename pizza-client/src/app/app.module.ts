import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RegisterComponent } from './register/register.component';
import {HttpClientModule} from "@angular/common/http";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import { TextInputComponent } from './_forms/text-input/text-input.component';
import { PizzaListComponent } from './pizzas/pizza-list/pizza-list.component';
import { PizzaItemComponent } from './pizzas/pizza-item/pizza-item.component';
import { PizzaDetailComponent } from './pizzas/pizza-detail/pizza-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    RegisterComponent,
    TextInputComponent,
    PizzaListComponent,
    PizzaItemComponent,
    PizzaDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
