import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../_environment/environment';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;

  currentUser = signal<User | null>(null);
  private http = inject(HttpClient);
  private router = inject(Router);

  login(model: any) {
    //Faz um post na API, usa o pipe para tratar o dado antes do subscribe, map transforma o retorno em um objeto User, coloca no localStorage e seta o currentUserSource com o user
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  redefinirSenha(model: any) {
    //Faz um post na API, usa o pipe para tratar o dado antes do subscribe, map transforma o retorno em um objeto User, coloca no localStorage e seta o currentUserSource com o user
    return this.http
      .post<User>(this.baseUrl + 'account/reset', model)
      .subscribe({
        next: (resp) => {},
        error: (resp) => {},
      });
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user) => {
        if (user) {
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
    this.router.navigateByUrl('/');
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role;
    //verifica se são várias roles ou uma só
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);

    localStorage.setItem('user', JSON.stringify(user));

    //com esse next todos os lugares onde usam o currentUserSource serão atualizados
    this.currentUser.set(user);
  }

  getDecodedToken(token: string) {
    //atob serve para decodificar 64bits
    return JSON.parse(atob(token.split('.')[1]));
  }
}
