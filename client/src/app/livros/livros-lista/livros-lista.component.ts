import {
  Component,
  ElementRef,
  inject,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { LivrosService } from '../../_services/livros.service';
import { ToastrService } from 'ngx-toastr';
import { PaginatedResut } from '../../_models/pagination';
import { Livro, LivroDto } from '../../_models/livro';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { DatePipe, NgFor, registerLocaleData } from '@angular/common';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { LivroModalComponent } from '../livro-modal/livro-modal.component';
import { SwalComponent } from '@sweetalert2/ngx-sweetalert2';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

//DatePipe, lingua no app.config (pt-BR)
@Component({
  selector: 'app-livros-lista',
  standalone: true,
  imports: [PaginationModule, DatePipe, SweetAlert2Module],
  templateUrl: './livros-lista.component.html',
  styleUrl: './livros-lista.component.css',
})
//Criar modal para registar
//Mudar a tabela para ordenar e colocar um filtro de busca
export class LivrosListaComponent implements OnInit {
  public livrosService = inject(LivrosService);
  private toastr = inject(ToastrService);
  livro: LivroDto | undefined;
  livroNome: any;
  livroId: any;

  @ViewChild('deleteSwal')
  public readonly deleteSwal!: SwalComponent;

  bsModalRef: BsModalRef<LivroModalComponent> =
    new BsModalRef<LivroModalComponent>();

  private modalService = inject(BsModalService);

  pageNumber = 1;
  pageSize = 10;
  //Retorna a lista de usuários e passa para o user-list pelo input dele

  ngOnInit(): void {
    this.livrosService.retornaLivros(this.pageNumber, this.pageSize);
  }

  abrirModalCadastro() {
    const initialState: ModalOptions = {
      class: 'modal-lg',
      initialState: {
        title: 'Cadastro de Livros',
        livro: this.livro,
      },
    };
    this.bsModalRef = this.modalService.show(LivroModalComponent, initialState);
  }

  //Paginação
  mudarPagina(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.livrosService.retornaLivros(this.pageNumber, this.pageSize);
    }
  }

  editarLivro(model: LivroDto) {
    const initialState: ModalOptions = {
      class: 'modal-lg',
      initialState: {
        title: 'Edição de Livros',
        livro: model,
      },
    };
    this.bsModalRef = this.modalService.show(LivroModalComponent, initialState);
  }

  excluirLivro(model: LivroDto) {
    this.livroNome = model.nome;
    this.livro = model;
    this.livroId = model.id;

    setTimeout(() => {
      this.deleteSwal.fire();
    }, 100);
  }

  confirmarExclusao(model: LivroDto) {
    this.livrosService.excluirLivro(model.id);
  }
}
