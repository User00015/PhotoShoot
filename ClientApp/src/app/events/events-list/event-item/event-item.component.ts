import { Component, OnInit, Output, Input, EventEmitter, HostListener } from '@angular/core';
import { EventsService } from "../../../services/events.service";
import { Event } from "../../../Models/event-model";
import { AuthService } from "../../../services/auth.service";
import { faTrash, faStar, faPencilAlt } from "@fortawesome/free-solid-svg-icons";
//import { faStar } from "@fortawesome/free-regular-svg-icons";

@Component({
  selector: 'app-event-item',
  templateUrl: './event-item.component.html',
  styleUrls: ['./event-item.component.scss']
})
export class EventItemComponent implements OnInit {
  faTrash = faTrash;
  faStar = faStar;
  faPencilAlt = faPencilAlt;
  exists: boolean = true;
  hovered: boolean = false;
  @Input() event: Event;
  @Output() onDelete: EventEmitter<any> = new EventEmitter();
  @Output() onEdit: EventEmitter<any> = new EventEmitter();
  @Output() onRegister: EventEmitter<any> = new EventEmitter();

  constructor(private authService: AuthService, private eventService: EventsService) { }

  delete(e: Event) {
    this.onDelete.emit(e);
  }

  register(e: Event) {
    this.onRegister.emit(e);
  }

  edit(e: Event) {
    this.onEdit.emit(e);
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
