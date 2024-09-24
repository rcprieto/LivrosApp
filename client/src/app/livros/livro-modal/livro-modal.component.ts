import { Component, inject, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Livro, LivroDto } from '../../_models/livro';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ReactiveFormsModule,
  FormsModule,
} from '@angular/forms';
import { AccountService } from '../../_services/account.service';
import { DatePickerComponent } from '../../_forms/date-picker/date-picker.component';
import { LivrosService } from '../../_services/livros.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-livro-modal',
  standalone: true,
  imports: [
    TextInputComponent,
    FormsModule,
    ReactiveFormsModule,
    DatePickerComponent,
  ],
  templateUrl: './livro-modal.component.html',
  styleUrl: './livro-modal.component.css',
})
export class LivroModalComponent implements OnInit {
  bsModalRef = inject(BsModalRef);
  title = '';
  private fb = inject(FormBuilder);
  registerForm: FormGroup = new FormGroup({});
  private accountService = inject(AccountService);
  private livroService = inject(LivrosService);

  livro: LivroDto | undefined;

  // livro: Livro = {
  //   id: 0,
  //   fimLeitura: null,
  //   inicioLeitura: null,
  //   nome: '',
  //   urlCapa: '',
  //   autor: '',
  //   resumo: '',
  //   appUser: null,
  // };

  ngOnInit(): void {
    this.inicializarFormulario();
  }

  inicializarFormulario() {
    this.registerForm = this.fb.group({
      id: 0,
      fimLeitura: [new Date().toLocaleDateString('pt-BR')],
      inicioLeitura: [new Date().toLocaleDateString('pt-BR')],
      nome: ['', Validators.required],
      urlCapa: [''],
      autor: [''],
      resumo: [''],
      appUserId: [this.accountService.currentUser()?.id],
      appUser: [null],
    });

    if (this.livro != undefined && this.livro.id > 0) {
      this.preencherFormulario();
    }
  }

  preencherFormulario() {
    this.registerForm.setValue(this.livro!);
    if (this.livro?.fimLeitura != null) {
      this.registerForm.controls['fimLeitura'].setValue(
        new Date(this.livro?.fimLeitura).toLocaleDateString('pt-BR')
      );
    }
    if (this.livro?.inicioLeitura != null) {
      this.registerForm.controls['inicioLeitura'].setValue(
        new Date(this.livro?.inicioLeitura).toLocaleDateString('pt-BR')
      );
    }
  }

  salvar() {
    const livro: LivroDto = this.registerForm.value;

    if (this.livro != undefined && this.livro.id > 0)
      this.livroService.atualizarLivro(livro);
    else this.livroService.cadastrarLivro(livro);

    this.bsModalRef.hide();
  }
}
