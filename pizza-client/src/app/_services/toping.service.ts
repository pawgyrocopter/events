import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Toping} from "../_models/toping";

@Injectable({
  providedIn: 'root'
})
export class TopingService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getTopings(){
    return this.http.get<Toping[]>(this.baseUrl + 'topings');
  }
}
