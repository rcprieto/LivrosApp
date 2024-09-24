import { Component, HostListener, inject, OnInit, signal } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { NgClass, NgIf } from '@angular/common';
import { UserListComponent } from '../user-list/user-list.component';
import { User } from '../../_models/user';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../_services/user.service';
import { PaginatedResut } from '../../_models/pagination';
import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';

@Component({
  selector: 'app-user-register',
  standalone: true,
  imports: [
    FormsModule,
    TextInputComponent,
    ReactiveFormsModule,
    NgIf,
    NgClass,
    UserListComponent,
  ],
  templateUrl: './user-register.component.html',
  styleUrl: './user-register.component.css',
  animations: [
    trigger('openClose', [
      // ...
      state(
        'open',
        style({
          opacity: 1,
        })
      ),
      state(
        'closed',
        style({
          height: '0px',
          display: 'none',
          opacity: 0.8,
        })
      ),
      transition('* => closed', [animate('0.2s')]),
      transition('* => open', [animate('0.2s')]),
    ]),
  ],
})
export class UserRegisterComponent implements OnInit {
  private userService = inject(UserService);
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  userServices = inject(UserService);
  state = 'closed';
  novoUsuario = true;

  pageNumber = 1;
  pageSize = 10;

  //Retorna a lista de usuários e passa para o user-list pelo input dele
  user: User = {
    id: '',
    userName: '',
    token: '',
    email: '',
    password: '',
    confirmPassword: '',
    roles: [],
    passwordAntigo: '',
  };

  avaliableRoles: any[] = ['Admin', 'User'];
  selectedRoles: any[] = [];

  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  //Serve para avisar o usuário que ele está saindo sem salvar
  // @HostListener('window:beforeunload', ['$event']) unloadNotification(
  //   $event: any
  // ) {
  //   if (this.registerForm.dirty) $event.returnValue = true;
  // }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.userService.getUserListPaginated(this.pageNumber, this.pageSize);
    this.cancelar();
  }

  //Cria um react form e inicializa os campos
  inicializarFormulario() {
    this.registerForm = this.fb.group({
      id: [''],
      token: [''],
      roles: [[]],
      userName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.minLength(4)]],
      passwordAntigo: ['', [Validators.minLength(4)]],
      confirmPassword: [
        '',
        [Validators.minLength(4), this.matchValues('password')],
      ],
    });

    //roda novamente o matchValues do confirmPassword se mudarem o password
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () =>
        this.registerForm.controls['confirmPassword'].updateValueAndValidity(),
    });
  }

  salvarCadastro() {
    this.registerForm.controls['roles'].setValue([]);
    this.registerForm.controls['roles'].setValue(this.selectedRoles);
    this.user = this.registerForm.value;

    //Cria usuário
    if (this.novoUsuario) {
      if (!this.user.password || this.user.password.length < 4) {
        this.toastr.error('Por favor informar uma senha.');
        return false;
      }
      this.userService.register(this.user).subscribe({
        error: (retorno) => {
          this.validationErrors = retorno;
          return false;
        },
        complete: () => {
          this.registerForm?.reset();
          this.toastr.success('Cadastrado com Sucesso!');
          this.cancelar();
          return true;
        },
      });
    } else {
      this.userService.updateUser(this.user).subscribe({
        error: (retorno) => {
          this.validationErrors = retorno;
          return false;
        },
        complete: () => {
          this.registerForm?.reset();
          this.toastr.success('Atualizado com Sucesso!');
          this.cancelar();
          return true;
        },
      });
    }
    return false;
  }

  preencherFormulario(model: User) {
    this.novoUsuario = false;
    this.user = model;
    this.selectedRoles = this.user.roles;
    this.registerForm.setValue(this.user);
    this.registerForm.controls['password'].setValue('');
    this.registerForm.controls['confirmPassword'].setValue('');
    this.registerForm.controls['passwordAntigo'].setValue('');
    this.state = 'open';
  }

  cancelar() {
    this.registerForm?.reset();
    this.selectedRoles = [];
    this.state = 'closed';
    this.novoUsuario = true;
  }

  //Paginação
  pagedChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.userService.getUserListPaginated(this.pageNumber, this.pageSize);
    }
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      //vai no pai do controle e busca o controle para comparar
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
    };
  }

  //Para selecionar as roles e coloca em uma array para salvar
  onCheckChange(event: any) {
    if (event.target.checked) this.selectedRoles.push(event.target.value);
    else
      this.selectedRoles.splice(
        this.selectedRoles.indexOf(event.target.value),
        1
      );

    return;
  }

  criarUsuario() {
    this.cancelar();
    this.state = 'open';
  }
}
