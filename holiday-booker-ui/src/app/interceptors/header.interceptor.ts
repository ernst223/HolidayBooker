import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

@Injectable()
export class HeaderInterceptor implements HttpInterceptor {
    constructor(private http: HttpClient, private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const headers = req.headers
        .set('Authorization', 'Bearer ' +  localStorage.getItem('access_token'));
        const authReq = req.clone({ headers, withCredentials: true });
        return next.handle(authReq).pipe(catchError((error, caught) => {
            console.log(error);
            this.handleAuthError(error);
            return of(error);
          }) as any);
        // let jsonReq: HttpRequest<any> = req.clone({
        //   setHeaders: {
        //     Authorization : `Bearer ${localStorage.getItem('access_token')}`
        //   }
        // });
        // return next.handle(jsonReq);
    }

    private handleAuthError(err: HttpErrorResponse): Observable<any> {
        if (err.status === 401) {
          console.log('handled error ' + err.status);
          this.router.navigate([`account/login`]);
          return of(err.message);
        }
        throw err;
    }
}
