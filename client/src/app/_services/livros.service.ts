import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../_environment/environment';
import { of, tap } from 'rxjs';
import { setPaginationHeader } from './paginationHelper';
import { Livro, LivroDto } from '../_models/livro';
import { PaginatedResut } from '../_models/pagination';
import { HelperService } from './helper.service';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class LivrosService {
  baseUrl = environment.apiUrl;
  http = inject(HttpClient);
  livros = signal<LivroDto[]>([]);
  public paginatedResults = signal<PaginatedResut<LivroDto[]>>({});
  helperService = inject(HelperService);
  private toastrService = inject(ToastrService);

  retornaLivros(pageNumber: number, pageSize: number) {
    let params = setPaginationHeader(pageNumber, pageSize);

    return this.http
      .get<LivroDto[]>(`${this.baseUrl}livro`, {
        observe: 'response',
        params,
      })
      .subscribe({
        next: (response) => {
          this.livros.set(response.body as LivroDto[]);
          this.paginatedResults.set({
            pagination: JSON.parse(response.headers.get('Pagination')!),
            result: this.livros(),
          });
        },
      });
  }

  retornaLivro(id: number) {
    const livro = this.livros().find((x) => x.id === id);
    if (livro !== undefined) return of(livro);

    return null;
  }

  cadastrarLivro(model: any) {
    model.fimLeitura = this.helperService.converterDataParaISO8601(
      model.fimLeitura
    );
    model.inicioLeitura = this.helperService.converterDataParaISO8601(
      model.inicioLeitura
    );

    return this.http
      .post<LivroDto>(`${this.baseUrl}livro/add-livro`, model)
      .subscribe({
        next: (item) => {
          console.log(item);
          this.livros().push(item);
          this.paginatedResults().result = this.livros();
          this.toastrService.success('Cadastro realizado com sucesso!');
        },
        error: (resp) => this.toastrService.error(resp),
      });
  }

  atualizarLivro(model: any) {
    //as vezes a data vem como dd/MM/yyyy (se não mexer no valor) e precisa alterar, ou vem no formato data escrita e já funciona.
    model.fimLeitura = this.helperService.converterDataParaISO8601(
      model.fimLeitura
    );
    model.inicioLeitura = this.helperService.converterDataParaISO8601(
      model.inicioLeitura
    );

    return this.http.put<LivroDto>(`${this.baseUrl}livro`, model).subscribe({
      next: (item) => {
        this.livros.update((l) =>
          l.map((m) => (m.id === model.id ? model : m))
        );
        this.paginatedResults().result = this.livros();
        this.toastrService.success('Cadastro atualizado com sucesso!');
      },
      error: (resp) => this.toastrService.error(resp),
    });
  }

  excluirLivro(id: number) {
    return this.http.delete(`${this.baseUrl}livro/${id}`).subscribe({
      next: () => {
        const index = this.livros().findIndex((livro) => livro.id === id);

        if (index !== -1) {
          this.livros().splice(index, 1);
          this.paginatedResults().result = this.livros();
        }
      },
    });
  }
}
