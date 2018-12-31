import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Event, Appointment } from "../Models/event-model";
import { environment } from "../../environments/environment";

@Injectable()
export class EventsService {

  private baseUrl = environment.baseUrl;
  private createEventUrl: string = this.baseUrl + "api/Events/Create";
  private deleteEventUrl: string = this.baseUrl + "api/Events/Delete";
  private getEventsUrl: string = this.baseUrl + "api/Events";
  private getAppointmentUrl: string = this.baseUrl + "api/Events/Appointment/";
  private getLocationsUrl: string = this.baseUrl + "v2/locations/";
  private confirmationUrl: string = this.baseUrl + "api/Square/Confirm";


  constructor(private http: HttpClient) { }


  create(newEvent: Event) {
    return this.http.post(this.createEventUrl, newEvent);
  }

  scheduleAppointment(appointment: Appointment) {
    return this.http.get<string>(`${this.getAppointmentUrl}${appointment.eventId}/${appointment.id}/checkout`);
  }

  confirmAppointment(transactionId: string, referenceId: string) {
    return this.http.get<boolean>(this.confirmationUrl, { params: { transactionId: transactionId, referenceId: referenceId } });
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
