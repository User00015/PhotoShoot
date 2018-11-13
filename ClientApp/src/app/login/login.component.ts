import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
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

  constructor(private authService: AuthService, private router: Router ) { }

  onSubmit() {
    this.error = null;
    this.authService.login({ username: this.login, password: this.password });
  }

  ngOnInit() {
    this.authService.loggedIn.subscribe((loggedIn) => {
      if (loggedIn) this.router.navigate(['/admin']);
    });
  }

}
