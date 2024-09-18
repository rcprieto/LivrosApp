import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';

export const routes: Routes = [

  {path: '', component: AppComponent},
  {path: 'user/user-register', component: UserRegisterComponent},


];
