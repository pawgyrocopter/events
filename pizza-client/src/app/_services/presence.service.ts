import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {BehaviorSubject, take} from "rxjs";
import {ToastrService} from "ngx-toastr";
import {Router} from "@angular/router";
import {User} from "../_models/user";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  hubUrl = environment.hubUrl;
  // @ts-ignore
  private hubConnection: HubConnection;
  private onlineUsersSource = new BehaviorSubject<string[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();

  constructor(private toastr: ToastrService, private router: Router) {
  }

  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'orders', {
        accessTokenFactory: () => user.token
      })
      .withAutomaticReconnect()
      .build();
    this.hubConnection
      .start()
      .catch(error => console.log(error));
  }

  stopHubConnection() {
    this.hubConnection.stop().catch(error => console.log(error))
  }
}
