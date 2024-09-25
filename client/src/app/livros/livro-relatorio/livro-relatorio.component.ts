import { Component, inject, OnInit } from '@angular/core';
import { LivrosService } from '../../_services/livros.service';

@Component({
  selector: 'app-livro-relatorio',
  standalone: true,
  imports: [],
  templateUrl: './livro-relatorio.component.html',
  styleUrl: './livro-relatorio.component.css',
})
export class LivroRelatorioComponent implements OnInit {
  private livroService = inject(LivrosService);

  ngOnInit(): void {}
}
