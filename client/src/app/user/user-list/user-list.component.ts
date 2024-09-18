import { Component, EventEmitter, inject, Input, input, OnInit, Output, Signal, signal } from '@angular/core';
import { UserService } from '../../_services/user.service';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { User } from '../../_models/user';
import { PaginatedResut } from '../../_models/pagination';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [PaginationModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit{


  @Output() selectedUser: EventEmitter<User> = new EventEmitter<User>();
  @Output() mudarPagina: EventEmitter<any> = new EventEmitter<any>();
  @Input() users: PaginatedResut<User[]> = {};




  ngOnInit(): void {

  }



  editUser(user: User)
  {
    user.password = "";
    user.confirmPassword = "";
    this.selectedUser.emit(user);
  }

  pageChanged(event: any)
  {
    this.mudarPagina.emit(event)
  }

}
