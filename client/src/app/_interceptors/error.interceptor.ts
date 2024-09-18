import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {

  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
   catchError(response =>
    {
      if(response){
        switch(response.status){
          case 400:
            if(response.error.errors){
              const modelStateErrors =[];
              for(const key in response.error.errors){
                if(response.error.errors[key])
                  {
                    modelStateErrors.push(response.error.errors[key]);
                  }
              }

              toastr.error(modelStateErrors.join(', '), response.status.toString())
              throw modelStateErrors.flat();
            }
            else{
              toastr.error(response.error.toString(), response.status.toString())
            }
            break;

            case 401:
              toastr.error('Sem Autorização', response.status.toString());
              break;

            case 404:
               router.navigateByUrl('/not-found');
               break;
            case 405:
                const navigationExtra3: NavigationExtras ={state: {error: response.error.error}};
                router.navigateByUrl('/server-error', navigationExtra3);
               break;
            case 500:
               const navigationExtras: NavigationExtras ={state: {error: response.error.details}};
               break;
            default:
                toastr.error('Algo deu errado');
                console.log(response);
                break;
        }
      }
      throw response;

    }
    )
  )
}
