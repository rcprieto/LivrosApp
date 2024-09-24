import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HelperService {
  converterDataParaISO8601(dataString: string) {
    const teste = dataString.toString().indexOf('/');
    if (teste > 0) {
      // Separa os componentes da data
      const partes = dataString.split('/');
      const dia = parseInt(partes[0]);
      const mes = parseInt(partes[1]) - 1; // Meses em JavaScript começam em 0
      const ano = parseInt(partes[2]);

      // Cria um novo objeto Date
      const data = new Date(ano, mes, dia);

      // Converte para ISO 8601
      return data.toISOString();
    } else {
      return dataString;
    }
  }
}
