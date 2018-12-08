import { Component, OnInit } from '@angular/core';
import { switchMap } from 'rxjs/operators';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { EventsService } from "../../services/events.service";
import { Appointment } from "../../Models/event-model";
import {Observable} from "rxjs";

@Component({
  selector: 'app-register-event',
  templateUrl: './register-event.component.html',
  styleUrls: ['./register-event.component.css']
})
export class RegisterEventComponent implements OnInit {

  constructor(private route: ActivatedRoute, private router: Router, private eventService: EventsService) { }

  appointments$: Observable<Appointment> = new Observable<Appointment>();

  ngOnInit() {
    console.log(this.route.snapshot.paramMap.get('id'));
    this.appointments$ = this.route.paramMap.pipe(
      switchMap((params: ParamMap) =>
        this.eventService.getAppointments(params.get('id')))
    );
  }

}
