import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Event, Appointment } from "../Models/event-model";

@Injectable()
export class EventsService {

  private createEventUrl: string = "https://localhost:5001/api/Events/Create";
  private deleteEventUrl: string = "https://localhost:5001/api/Events/Delete";
  private getEventsUrl: string = "https://localhost:5001/api/Events";
  private getAppointmentUrl: string = "https://localhost:5001/api/Events/Appointment/";

  constructor(private http: HttpClient) { }

  private httpOptions = {
    params: {}
  };

  create(newEvent: Event) {
    return this.http.post(this.createEventUrl, newEvent);

  }

  getAppointments(id: string): any {
    return this.http.get<Appointment>(`${this.getAppointmentUrl}${id}`);
  }

  delete(eventId: string) {
    return this.http.delete(this.deleteEventUrl, { params: { "id": eventId } });
  }

  getEvents() {
    return this.http.get<Event>(this.getEventsUrl);

  }

}
