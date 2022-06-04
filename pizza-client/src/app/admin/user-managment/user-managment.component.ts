import { Component, OnInit } from '@angular/core';
import {BsModalRef, BsModalService} from "ngx-bootstrap/modal";
import {User} from "../../_models/user";
import {AdminService} from "../../_services/admin.service";
import {RolesModalComponent} from "../../modals/roles-modal/roles-modal.component";

@Component({
  selector: 'app-user-managment',
  templateUrl: './user-managment.component.html',
  styleUrls: ['./user-managment.component.css']
})
export class UserManagmentComponent implements OnInit {
  bsModalRef: BsModalRef;
  users: Partial<User[]>;
  constructor(private adminService: AdminService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUserWithRoles().subscribe(response => {
      this.users = response;
    })
  }

  openRolesModal(user: User) {
    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        user,
        roles: this.getRolesArray(user)
      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    // @ts-ignore
    this.bsModalRef.content.updateSelectedRoles.subscribe(value => {
      const rolesToUpdate = {
        roles: [...value.filter((el: { checked: any; }) => el.checked).map((el: { name: any; }) => el.name)]
      };
      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user.name, rolesToUpdate.roles).subscribe(() => {
          user.roles = [...rolesToUpdate.roles];
        })
      }
    })
  }

  private getRolesArray(user: User) {
    // @ts-ignore
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name: 'Customer', value: 'Customer'},
      {name: 'PizzaMaker', value: 'PizzaMaker'},
      {name: 'Admin', value: 'Admin'},
    ];

    availableRoles.forEach(role => {
      let isMatch = false;
      for (const userRole of userRoles) {
        if (role.name === userRole) {
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }
      }
      if (!isMatch) {
        role.checked = false;
        roles.push(role);
      }
    });
    // @ts-ignore
    return roles;
  }

}
