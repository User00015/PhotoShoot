import {CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuardService implements CanActivate {
  canActivate() {
    console.log("saw route change");
    return true;
  }

  constructor() { }

}
