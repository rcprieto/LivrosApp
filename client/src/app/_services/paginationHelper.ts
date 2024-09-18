import { HttpClient, HttpParams } from "@angular/common/http";
import { map, tap } from "rxjs";
import { PaginatedResut } from "../_models/pagination";


export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
  const paginatedResult:  PaginatedResut<T> = new PaginatedResut<T>;

  return http.get<T>(url, { observe: 'response', params }).pipe(
    tap(response => {

      if (response.body) {
        paginatedResult.result = response.body;
      }
      const pagination = response.headers.get('Pagination');

      if (pagination)
        paginatedResult.pagination = JSON.parse(pagination);

      return paginatedResult;

    })
  );
}

export function  setPaginationHeader(pageNumber: number, pageSize: number) {
  let params = new HttpParams();

  if (pageNumber && pageSize) {
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);
  }

  return params;
}
