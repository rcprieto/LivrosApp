import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

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

  ngOnInit(): void {
    if (this.accountService.currentUser() != null) {
      this.router.navigate(['/livros/livros-lista']);
    }
  }

  login() {
    this.accountService.login(this.model).subscribe({
      next: () => this.router.navigate(['/livros/livros-lista']),
      error: (error) => console.log(error),
    });
  }

  redefinir() {
    this.accountService.redefinirSenha(this.modelRedefinir);

    this.cancelarRedefinir();
  }

  esqueceuSenha() {
    this.redefinirSenha = true;
  }
  cancelarRedefinir() {
    this.redefinirSenha = false;
  }
}
