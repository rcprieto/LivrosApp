import {
  Component,
  EventEmitter,
  inject,
  Input,
  input,
  OnInit,
  output,
  Output,
  Signal,
  signal,
} from '@angular/core';
import { UserService } from '../../_services/user.service';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { User } from '../../_models/user';
import { PaginatedResut } from '../../_models/pagination';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [PaginationModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css',
})
export class UserListComponent implements OnInit {
  mudarPagina = output<any>();
  selectedUser = output<User>();
  //@Input() users: PaginatedResut<User[]> = {};
  users = input.required<PaginatedResut<User[]> | null>();

  ngOnInit(): void {}

  editUser(user: User) {
    user.password = '';
    user.confirmPassword = '';
    user.passwordAntigo = '';
    this.selectedUser.emit(user);
  }

  pageChanged(event: any) {
    this.mudarPagina.emit(event);
  }
}
