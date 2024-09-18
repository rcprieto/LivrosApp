import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{

  livros: any;
  http = inject(HttpClient);
  title = 'Livros';
  private accountService = inject(AccountService);

  ngOnInit(): void {
    this.setCurrentUser();
    // this.http.get(`https://localhost:5001/api/livro/get-livros`).subscribe(
    //   {
    //     next: (response) => { this.livros = response },
    //     error: error => console.log(error),
    //     complete: () => {},
    //   }
    // )

  }

  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if(!userString) return;
    const user: User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);

  }
}
