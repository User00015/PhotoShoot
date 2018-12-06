import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Event } from "../Models/event-model";

@Injectable()
export class EventsService {

  private createEventUrl: string = "https://localhost:5001/api/Events/Create";
  private deleteEventUrl: string = "https://localhost:5001/api/Events/Delete";
  private getEventsUrl: string = "https://localhost:5001/api/Events";

  constructor(private http: HttpClient) { }

  private httpOptions = {
    params: {}
  };

  create(newEvent: Event) {
    return this.http.post(this.createEventUrl, newEvent);

  }

  delete(eventId: string) {
    return this.http.delete(this.deleteEventUrl, { params: { "id": eventId } });
  }

  getEvents() {
    return this.http.get<Event>(this.getEventsUrl);

  }

}
