import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../_environment/environment';
import { BehaviorSubject, map, tap } from 'rxjs';
import { User } from '../_models/user';
import { getPaginatedResult, setPaginationHeader } from './paginationHelper';
import { AccountService } from './account.service';
import { PaginatedResut } from '../_models/pagination';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = environment.apiUrl;

  private http = inject(HttpClient);
  accountService = inject(AccountService);
  users = signal<User[]>([]);
  paginatedResults = signal<PaginatedResut<User[]>>({ result: this.users() });

  getUserListPaginated(pageNumber: number, pageSize: number) {
    let params = setPaginationHeader(pageNumber, pageSize);
    //params = params.append('orderBy', 'username');

    return this.http
      .get<User[]>(this.baseUrl + 'users', {
        observe: 'response',
        params,
      })
      .subscribe({
        next: (response) => {
          this.users.set(response.body as User[]);
          this.paginatedResults.set({
            pagination: JSON.parse(response.headers.get('Pagination')!),
            result: this.users(),
          });
        },
      });

    //this.http.get(this.baseUrl + 'users', this.getHttpOptions())
  }

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + 'admin/user-with-roles');
  }

  updateUserRoles(username: string, roles: string) {
    return this.http.post<string[]>(
      this.baseUrl + 'admin/edit-roles/' + username + '?roles=' + roles,
      {}
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'users/register', model).pipe(
      tap((user) => {
        this.users().push(user);
        this.paginatedResults.set({ result: this.users() });
      })
    );
  }

  updateUser(model: any) {
    return this.http.put<User>(this.baseUrl + 'users', model).pipe(
      tap(() => {
        this.users.update((l) => l.map((m) => (m.id === model.id ? model : m)));
        this.paginatedResults.set({ result: this.users() });
      })
    );
  }

  // getHttpOptions(){
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: `Bearer ${accountService.currentUser()?.token}`
  //     })
  //   }

  // }
}
