import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable()
export class appInterceptor implements HttpInterceptor{
  constructor(){}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const storedToken = localStorage.getItem("token");

let token: string | null = null;

if (storedToken) {
  try {
    token = JSON.parse(storedToken);
  } catch (error) {
    console.error('Error parsing stored token:', error);
  }
}
    if(token){
      req = req.clone({
        setHeaders: { Authorization: `Bearer ${token}` }
      })
    }

    return next.handle(req);
  }
}