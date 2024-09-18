import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { CommonModule} from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
//Dropdowns verificar https://valor-software.com/ngx-bootstrap/#/components/dropdowns?tab=api  app.config animations
@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule, CommonModule, BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  model: any ={};

  public accountService = inject(AccountService);
  private router = inject(Router);

  login() {

    this.accountService.login(this.model).subscribe({
      next: (response) => {console.log(response)},
      error: error => console.log(error)
    });
  }

  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
