import { Component, OnInit, Output, Input, EventEmitter, HostListener } from '@angular/core';
import { EventsService } from "../../../services/events.service";
import { Event } from "../../../Models/event-model";
import { AuthService } from "../../../services/auth.service";
import { faTrash, faStar } from "@fortawesome/free-solid-svg-icons";
//import { faStar } from "@fortawesome/free-regular-svg-icons";

@Component({
  selector: 'app-event-item',
  templateUrl: './event-item.component.html',
  styleUrls: ['./event-item.component.scss']
})
export class EventItemComponent implements OnInit {
  faTrash = faTrash;
  faStar = faStar;
  exists: boolean = true;
  hovered: boolean = false;
  @Input()
  event: Event;
  @Output()
  onDelete: EventEmitter<any> = new EventEmitter();
  @Output()
  onRegister: EventEmitter<any> = new EventEmitter();

  link = "https://www.google.com/maps/search/?api=1&query=";
  googleMapLink;

  constructor(private authService: AuthService, private eventService: EventsService) { }

  delete(e: Event) {
    this.onDelete.emit(e);
  }

  register(e: Event) {
    this.onRegister.emit(e);
  }


  @HostListener('mouseenter', ['$event']) onMouseEnter($event) {
    $event.preventDefault();
    this.hovered = true;
  }

  @HostListener('mouseleave', ['$event']) onMouseLeave($event) {
    $event.preventDefault();
    this.hovered = false;

  }

  ngOnInit() {
    this.googleMapLink = `${this.link}${this.event.address}`;
  }

}
