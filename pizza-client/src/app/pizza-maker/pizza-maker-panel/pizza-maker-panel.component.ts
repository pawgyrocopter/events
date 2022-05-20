import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-pizza-maker-panel',
  templateUrl: './pizza-maker-panel.component.html',
  styleUrls: ['./pizza-maker-panel.component.css']
})
export class PizzaMakerPanelComponent implements OnInit {

  constructor(private http : HttpClient) { }

  ngOnInit(): void {
  }

}
