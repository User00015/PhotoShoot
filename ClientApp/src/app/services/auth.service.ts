import { Injectable} from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable, Subject} from "rxjs";
import {ILoginCredentials} from "../Models/login-credentials"

@Injectable()
export class AuthService {

  private url:string = "https://localhost:5001/api/Account/Authenticate";

  loggedIn:Subject<boolean> = new Subject<boolean>();

  login(params: ILoginCredentials) {
    let result = this.http.post(this.url, params);
    result.subscribe(authToken => {
      this.loggedIn.next(true);
      localStorage.setItem("auth-token", JSON.stringify(authToken));
    }, err => {
      this.loggedIn.next(false);
      localStorage.removeItem("auth-token");
    });
  }

  isLoggedIn() {
    return this.loggedIn;

  }

  constructor(private http: HttpClient) {
    let authToken = localStorage.getItem("auth-token");
    authToken ? this.loggedIn.next(true) : this.loggedIn.next(false);
  }

}
