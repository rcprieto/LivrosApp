import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { delay, finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const busyService = inject(BusyService);
  busyService.busy();
  return next(req).pipe(
    delay(200),
    finalize(() => {
      busyService.idle();
    })
  );
};

//https://napster2210.github.io/ngx-spinner/   https://github.com/Napster2210/ngx-spinner?tab=readme-ov-file#readme
//npm install ngx-spinner --save
//ng g interceptor _interceptors/loading --skip-tests
//No angular.json styles
//"./node_modules/ngx-spinner/animations/square-jelly-box.css",
