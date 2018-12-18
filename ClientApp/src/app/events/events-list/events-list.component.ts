import { Component, OnInit } from '@angular/core';
import {EventsService} from "../../services/events.service";
import { Observable, Subject } from "rxjs";
import { Event } from "../../Models/event-model";
import { AuthService } from "../../services/auth.service";
import { startWith, switchMap  } from 'rxjs/operators';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-events-list',
  templateUrl: './events-list.component.html',
  styleUrls: ['./events-list.component.css']
})
export class EventsListComponent implements OnInit {

  events$: Observable<Event> = new Observable<Event>();
  subject: Subject<Event> = new Subject<Event>();

  constructor(private eventService: EventsService, private authService: AuthService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.events$ = this.subject.asObservable().pipe(
      startWith(null),
      switchMap(() => this.eventService.getEvents())
    );
  }

  handleRegister(event: Event) {
    this.router.navigate(['./register', event.id], {relativeTo: this.route});

  }

  handleDelete(event: Event) {
    this.eventService.delete(event.id).subscribe(() => this.subject.next());
  }

}
