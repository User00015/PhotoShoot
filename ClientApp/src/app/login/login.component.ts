import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {AuthService} from '../services/auth.service';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  password: string;
  login: string;

  errors$: Observable<string> = this.authService.errors.pipe(map((e: any) => e.error.message));
  error: string;
  constructor(private authService: AuthService, private router: Router ) { }

  onSubmit(loginForm) {
    this.authService.login({ username: this.login, password: this.password });
    loginForm.reset();
  }

  ngOnInit() {
    this.authService.loggedIn.subscribe((loggedIn) => {
      if (loggedIn) this.router.navigate(['/admin']);
    });
  }
}
