import { Component, Inject, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { UserService } from '../../_services/user.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { UserEdit } from '../../_models/user';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { NgClass, NgIf } from '@angular/common';
import { UserListComponent } from '../user-list/user-list.component';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-edit',
  standalone: true,
  imports: [
    FormsModule,
    TextInputComponent,
    ReactiveFormsModule,
    NgIf,
    NgClass,
    UserListComponent,
  ],
  templateUrl: './user-edit.component.html',
  styleUrl: './user-edit.component.css',
})
export class UserEditComponent implements OnInit {
  private accountService = inject(AccountService);
  private userService = inject(UserService);
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  private router = inject(Router);
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;
  user: UserEdit = {
    id: '',
    userName: '',
    email: '',
    password: '',
    passwordAntigo: '',
    confirmPassword: '',
  };

  ngOnInit(): void {
    this.popularFormulario();
  }

  popularFormulario() {
    this.registerForm = this.fb.group({
      id: [this.accountService.currentUser()!.id],
      userName: [
        this.accountService.currentUser()!.userName,
        Validators.required,
      ],
      email: [this.accountService.currentUser()!.email],
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

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      //vai no pai do controle e busca o controle para comparar
      return control.value === control.parent?.get(matchTo)?.value
        ? null
        : { notMatching: true };
    };
  }

  salvarCadastro() {
    this.user = this.registerForm.value;

    this.userService.updateCadastro(this.user).subscribe({
      next: (resp) => {
        this.toastr.success('Atualizado com Sucesso! Faça login novamente.');
        this.accountService.logout();
        return true;
      },
      error: (erro) => {
        this.validationErrors = erro;
        this.toastr.error(erro);
        return false;
      },
      complete: () => {},
    });

    return false;
  }

  cancelar() {
    if (this.accountService.currentUser() != null) {
      this.router.navigate(['/livros/livros-lista']);
    }
  }
}
