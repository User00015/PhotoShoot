import { Component, OnInit, NgZone } from '@angular/core';
import { Event, Appointment } from "../../Models/event-model";
import {EventsService} from "../../services/events.service";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import * as _ from 'lodash';
const diffInMinutes = require('date-fns/difference_in_minutes');
const addMinutes = require('date-fns/add_minutes');


@Component({
  selector: 'app-create',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.scss']
})
export class CreateEventComponent implements OnInit {

  ngOnInit(): void {
    this.model.startTime = { hour: 18, minute: 0 }
    this.model.endTime = { hour: 20, minute: 0 }
  }

  //TODO - Add private option at some point.

  public faCalendar = faCalendar;

  public model: Event = new Event();
  formData: FormData = new FormData();
  //public appointmentTimes: Appoin[] = [];
  public imagePreview: File;
  public timePerSlot: number;

  handleDrop(fileList: FileList) {

    let file = _.head(fileList);

    var reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = (event: any) => {
      this.imagePreview = event.target.result;
      this.model.image = event.target.result;
    }

  }

  setAddress(address) {
    this.zone.run(() => {
      this.model.address = address;
    });
  }
  constructor(private zone: NgZone, private eventService: EventsService) { }

  generateAppointmentSlots() {
    this.model.appointments = [];

    //Keep track of number of slots to generate, and the time offset
    let count = 0;
    let timeCount = this.timePerSlot;

    //Get start and end dates based on model. This is hacky, but it works.
    let startDate = new Date(this.model.startDate.year,
      this.model.startDate.month,
      this.model.startDate.day,
      this.model.startTime.hour,
      this.model.startTime.minute);

    let endDate = new Date(this.model.endDate.year,
      this.model.endDate.month,
      this.model.endDate.day,
      this.model.endTime.hour,
      this.model.endTime.minute);

    
    let numberOfAppointments = diffInMinutes(endDate, startDate) / this.timePerSlot;

    //Iterate through each appointment, create the appropriate values then add them to the model. This is doing to much work. TODO - FIXME
    _.times(numberOfAppointments, () => {
      let appointment: Appointment = new Appointment();
      appointment.id = count++;
      appointment.display = addMinutes(startDate, timeCount);
      this.model.appointments.push(appointment);
      timeCount += this.timePerSlot;
    });
  }

  onSubmit() {
    this.eventService.create(this.model).subscribe(res => console.log(res));
  }


}
