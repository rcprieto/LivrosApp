import { Component, inject, OnInit, signal } from '@angular/core';
import { LivrosService } from '../../_services/livros.service';
import { NgApexchartsModule } from 'ng-apexcharts';
import { ReportService } from '../../_services/report.service';
import { ReportsDto } from '../../_models/reports';

@Component({
  selector: 'app-livro-relatorio',
  standalone: true,
  imports: [NgApexchartsModule],
  templateUrl: './livro-relatorio.component.html',
  styleUrl: './livro-relatorio.component.css',
})
export class LivroRelatorioComponent implements OnInit {
  private livroService = inject(LivrosService);
  private reportService = inject(ReportService);
  data = signal<ReportsDto>({
    totalLivros: 0,
    totalLivrosAno: 0,
    totalPaginas: 0,
    totalPaginasAno: 0,
    reportListaLivros: [],
    paginasPorMes: [],
  });

  labels: string[] = [
    'Jan',
    'Feb',
    'Mar',
    'Apr',
    'May',
    'Jun',
    'Jul',
    'Ago',
    'Set',
    'Out',
    'Nov',
    'Dez',
  ];
  series: any = [
    {
      name: 'Páginas',
      data: [],
    },
  ];
  ngOnInit(): void {
    this.chartPaginasPorMes();
  }

  chartOptions: any = {
    chart: {
      height: 350,
      type: 'line',
      dropShadow: {
        enabled: true,
        color: '#000',
        top: 18,
        left: 7,
        blur: 10,
        opacity: 0.2,
      },
    },
    toolbar: {
      show: true,
    },
    dataLabels: {
      enabled: true,
    },
    stroke: {
      curve: 'smooth',
    },
    title: {
      text: 'Páginas Por Mês',
      align: 'left',
    },
    grid: {
      borderColor: '#e7e7e7',
      row: {
        colors: ['#f3f3f3', 'transparent'], // takes an array which will be repeated on columns
        opacity: 0.5,
      },
    },
    markers: {
      size: 1,
    },

    xaxis: {
      categories: this.labels,
    },
  };

  chartPaginasPorMes() {
    this.reportService.reportDash(2024, 9).subscribe({
      next: (resp) => {
        this.data.set(resp);
        this.series = [
          {
            name: 'Páginas',
            data: resp.paginasPorMes,
          },
        ];
      },
    });
  }
}
