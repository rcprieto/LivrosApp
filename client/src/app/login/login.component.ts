import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent implements OnInit {
  model: any = {};
  modelRedefinir: any = {};
  redefinirSenha = false;

  public accountService = inject(AccountService);
  private router = inject(Router);
  private toastr = inject(ToastrService);

  ngOnInit(): void {
    if (this.accountService.currentUser() != null) {
      this.router.navigate(['/livros/livros-relatorio']);
    }
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigate(['/livros/livros-relatorio']),
      error: (error) => this.toastr.error(error),
    });
  }

  redefinir() {
    this.accountService.redefinirSenha(this.modelRedefinir);
    this.cancelarRedefinir();
    this.toastr.success('Senha redefinida, verifique seu email');
  }

  esqueceuSenha() {
    this.redefinirSenha = true;
  }
  cancelarRedefinir() {
    this.redefinirSenha = false;
    this.modelRedefinir = {};
  }
}
