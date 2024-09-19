import { CanActivateFn } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const adminGuard: CanActivateFn = (route, state) => {

  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  if(accountService.currentUser() != null)
  {
    if(accountService.currentUser()?.roles.includes('Admin') )
      return true;
  }

  toastr.error('Acesso negado');

  return false;

};
