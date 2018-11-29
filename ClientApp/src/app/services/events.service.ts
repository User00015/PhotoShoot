import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { Event } from "../Models/event-model";

@Injectable()
export class EventsService {
  
  private createEventUrl: string = "https://localhost:5001/api/Events/Create";

  constructor(private http: HttpClient) { }

  create(newEvent: Event) {
    return this.http.post(this.createEventUrl, newEvent);

  }

}
