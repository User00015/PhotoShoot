import { Component, OnInit } from '@angular/core';
import {AuthService} from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  password: string;
  login: string;

  error;

  constructor(private authService: AuthService ) { }

  onSubmit() {
    this.error = null;
    this.authService.login({ username: this.login, password: this.password });
  }

  ngOnInit() {
      this.authService.isLoggedIn().subscribe(
        credentials => console.log(credentials),
        err => { this.error = err.error});
  }

}
