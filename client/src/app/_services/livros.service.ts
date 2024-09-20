import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../_environment/environment';
import { BehaviorSubject, map, tap } from 'rxjs';
import { User } from '../_models/user';
import { getPaginatedResult, setPaginationHeader } from './paginationHelper';
import { AccountService } from './account.service';
import { Livro } from '../_models/livro';

@Injectable({
  providedIn: 'root',
})
export class LivrosService {
  baseUrl = environment.apiUrl;
  http = inject(HttpClient);

  retornaLivros(pageNumber: number, pageSize: number) {
    let params = setPaginationHeader(pageNumber, pageSize);

    return this.http.get<Livro[]>(`${this.baseUrl}livro`, {
      observe: 'response',
      params,
    });
  }

  cadastrarLivro(model: Livro) {
    return this.http.post<Livro>(`${this.baseUrl}livro/add-livro`, model).pipe(
      tap((item) => {
        return item;
      })
    );
  }

  atualizarLivro(model: Livro) {
    return this.http.put<Livro>(`${this.baseUrl}livro`, model).pipe(
      tap((item) => {
        return item;
      })
    );
  }
}
