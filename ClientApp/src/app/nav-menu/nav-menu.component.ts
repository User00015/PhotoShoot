import { Component, OnInit } from '@angular/core';
import { faHome, faImage, faCalendarAlt, faDirections } from '@fortawesome/free-solid-svg-icons';
import { AuthService } from "../services/auth.service";
import {Observable} from "rxjs/Observable";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit{

isLoggedIn:Observable<boolean>;

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
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

  constructor(private authService: AuthService) {  }
}
