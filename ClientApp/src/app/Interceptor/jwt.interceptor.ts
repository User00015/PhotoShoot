import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
 
@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add authorization header with jwt token if available
    let authToken = JSON.parse(localStorage.getItem('auth-token'));
    if (authToken && authToken.token) {
      request = request.clone({
        setHeaders: { 
          Authorization: `Bearer ${authToken.token}`,
        }
      });
    }
 
    return next.handle(request);
  }
}
