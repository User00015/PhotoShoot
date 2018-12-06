import { Component, OnInit } from '@angular/core';
import {EventsService} from "../../services/events.service";
import { Observable, Subject } from "rxjs";
import { Event } from "../../Models/event-model";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {

  events$: Observable<Event> = new Observable<Event>();

  constructor(private eventService: EventsService, private authService: AuthService) { }

  ngOnInit() {
    this.events$ = this.eventService.getEvents();
  }

}
