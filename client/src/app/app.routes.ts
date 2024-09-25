import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { authGuard } from './_guards/auth.guard';
import { adminGuard } from './_guards/admin.guard';
import { LivrosListaComponent } from './livros/livros-lista/livros-lista.component';
import { LoginComponent } from './login/login.component';
import { UserEditComponent } from './user/user-edit/user-edit.component';
import { LivroRelatorioComponent } from './livros/livro-relatorio/livro-relatorio.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [authGuard],
    children: [
      { path: 'livros/livros-lista', component: LivrosListaComponent },
      { path: 'livros/livros-relatorio', component: LivroRelatorioComponent },
      {
        path: 'user/user-register',
        component: UserRegisterComponent,
        canActivate: [adminGuard],
      },
      { path: 'user/user-edit', component: UserEditComponent },
      { path: '**', component: AppComponent, pathMatch: 'full' },
    ],
  },
];
