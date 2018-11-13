import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, BehaviorSubject } from "rxjs";
import { ILoginCredentials } from "../Models/login-credentials"

@Injectable()
export class AuthService {

  private url: string = "https://localhost:5001/api/Account/Authenticate";

  public loggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

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

  logOut() {
    localStorage.removeItem("auth-token");
    this.loggedIn.next(false);
  }

  constructor(private http: HttpClient) {
    let authToken = localStorage.getItem("auth-token");
    authToken ? this.loggedIn.next(true) : this.loggedIn.next(false);
  }

}
