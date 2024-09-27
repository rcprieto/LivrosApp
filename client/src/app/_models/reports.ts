export class ReportsDto {
  totalLivros: number = 0;
  totalLivrosAno: number = 0;
  totalPaginas: number = 0;
  totalPaginasAno: number = 0;
  reportListaLivros: ReportListaLivros[] = [];
  paginasPorMes: number[] = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
}

export class ReportListaLivros {
  nome?: string;
  data?: string;
  paginas?: number;
  genero?: string;
  autor?: string;
}
