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
  faStar  = faStar ;
  exists: boolean = true;
  hovered: boolean = false;
  @Input() event: Event;
  constructor(private authService: AuthService, private eventService: EventsService) { }

  onDelete(e: Event) {
    console.log("called delte");
    //this.eventService.delete(e.id).subscribe(() => {
    //  this.exists = false;
    //});
  }

  selected(e: Event) {
    console.log(e);
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
  }

}
