import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from "@angular/router";
import { Injectable } from '@angular/core';
import { AuthService } from "./auth.service";

@Injectable()
export class AuthGuardService implements CanActivate {
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authService.loggedIn.value) {
      return true;
    } else {
      this.router.navigate(['/']);
    }

    //return this.authService.loggedIn.value ? true : this.router.navigate(['/']);
  }

  constructor(private authService: AuthService, private router: Router) { }

}
