import { Injectable } from '@angular/core';
import { CanActivate,ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
@Injectable({
  providedIn: 'root',
})
export class DashGuard implements CanActivate {
  constructor(private router:Router){}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    const token=localStorage.getItem('token');
    if(token){
      return true
    }else{
      this.router.navigate(['/']);
      return false;
    }
  }

};
