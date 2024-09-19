import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { authGuard } from './_guards/auth.guard';
import { adminGuard } from './_guards/admin.guard';

export const routes: Routes = [

  {path: '', component: AppComponent},
  {path: '',
    runGuardsAndResolvers: 'always',
    canActivate:[authGuard],
    children: [
      {path: 'user/user-register', component: UserRegisterComponent, canActivate: [adminGuard]},
      {path: '**', component: AppComponent, pathMatch: 'full'}
      ]
  }


];
