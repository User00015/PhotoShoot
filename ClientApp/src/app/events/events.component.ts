import { Component, OnInit } from '@angular/core';
import { AuthService } from "../services/auth.service";
import { Subject } from "rxjs/Subject";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  constructor(private authService: AuthService) { }

  isLoggedIn: Subject<boolean>;

  ngOnInit() {
    this.isLoggedIn = this.authService.loggedIn;
  }

}
