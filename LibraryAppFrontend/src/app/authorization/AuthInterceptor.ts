import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, 
HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from './AuthService';

@Injectable({
 providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {

   constructor(private service:AuthService, private router:Router){}

   intercept(req: HttpRequest<any>, next: HttpHandler): 
   Observable<HttpEvent<any>>{

    if(req.url.search("login")===-1 && req.url.search("tset")===-1){
      var token = this.service.getToken();    
      if (token) {
         req = req.clone({
         setHeaders: {
         Authorization: 'Bearer '+token
         }
    });
     }
     return next.handle(req).pipe(
      catchError((error) => {
      console.log(error);
      if (error instanceof HttpErrorResponse && error.status === 401) {
            this.router.navigate(['login']);
      }
      return throwError(()=>new Error(error));
      }));
    }

    return next.handle(req);
   }

}