import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../_environment/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ReportsDto } from '../_models/reports';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  baseUrl = environment.apiUrl;
  http = inject(HttpClient);

  reportDash(ano: number, mes: number) {
    let params = new HttpParams();

    params = params.append('ano', ano);
    params = params.append('mes', mes);

    return this.http.get<ReportsDto>(`${this.baseUrl}report/dash`, { params });
  }
}
