import { Component, OnInit } from '@angular/core';
import { faHome, faImage, faCalendarAlt, faDirections } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from "../services/auth.service";
import { Observable, Subject } from "rxjs";
import { last } from 'rxjs/operators';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  isLoggedIn: Subject<boolean>;

  ngOnInit(): void {
    this.isLoggedIn = this.authService.loggedIn;
  }

  logout(): void {
    this.authService.logOut();
  }

  faHome = faHome;
  faImage = faImage;
  faCalendarAlt = faCalendarAlt;
  faDirections = faDirections;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  constructor(private authService: AuthService) {}

}
