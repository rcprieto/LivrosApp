import { Component, inject, OnInit, signal } from '@angular/core';
import { LivrosService } from '../../_services/livros.service';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResut } from '../../_models/pagination';
import { Livro } from '../../_models/livro';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { DatePipe, registerLocaleData } from '@angular/common';
import { Config } from 'datatables.net';

//DatePipe, lingua no app.config (pt-BR)
@Component({
  selector: 'app-livros-lista',
  standalone: true,
  imports: [PaginationModule, DatePipe],
  templateUrl: './livros-lista.component.html',
  styleUrl: './livros-lista.component.css',
})
export class LivrosListaComponent implements OnInit {
  livrosService = inject(LivrosService);
  private toastr = inject(ToastrService);
  dtOptions: Config = {};

  pageNumber = 1;
  pageSize = 10;
  //Retorna a lista de usuários e passa para o user-list pelo input dele
  paginatedResults = signal<PaginatedResut<Livro[]>>({});

  ngOnInit(): void {
    this.retornaLivros();
    this.dtOptions = {
      pagingType: 'full_numbers',
    };
  }

  //Paginação
  mudarPagina(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.retornaLivros();
    }
  }

  retornaLivros() {
    this.livrosService.retornaLivros(this.pageNumber, this.pageSize).subscribe({
      next: (response) => {
        //popular o paginatedResults com os livros e pega do header os dados para paginação
        this.paginatedResults.set({
          pagination: JSON.parse(response.headers.get('Pagination')!),
          result: response.body as Livro[],
        });
      },
    });
  }

  editarLivro(model: Livro) {}
}
