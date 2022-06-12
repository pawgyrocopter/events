import {Component, EventEmitter, Input, OnInit} from '@angular/core';
import {BsModalRef} from "ngx-bootstrap/modal";
import {Pizza, State} from "../../_models/pizza";

@Component({
  selector: 'app-state-modal',
  templateUrl: './state-modal.component.html',
  styleUrls: ['./state-modal.component.css']
})
export class StateModalComponent implements OnInit {
  @Input() updateState = new EventEmitter();
  pizza: Pizza
  states = [
    State.Pending,
    State["In Progress"],
    State.Ready,
    State.Canceled
  ];

  constructor(public bsModalRef: BsModalRef) {
  }

  ngOnInit(): void {
  }

  updateRoles() {
    this.updateState.emit(this.states);
    this.bsModalRef.hide();
  }

}
