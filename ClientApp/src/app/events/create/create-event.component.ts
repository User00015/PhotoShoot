import { Component, OnInit, NgZone } from '@angular/core';
import { Event, Appointment, Date as DateModel, Time } from "../../Models/event-model";
import { EventsService } from "../../services/events.service";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import * as _ from 'lodash';
const diffInMinutes = require('date-fns/difference_in_minutes');
const addMinutes = require('date-fns/add_minutes');
const format = require('date-fns/format');


@Component({
  selector: 'app-create',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.scss']
})
export class CreateEventComponent implements OnInit {

  //TODO - Add private option at some point.

  public faCalendar = faCalendar;



  public model: Event = new Event();
  public formStartDate: DateModel;
  public formEndDate: DateModel;
  public formStartTime: Time;
  public formEndTime: Time;

  public timePerSlot: number;


  formData: FormData = new FormData();

  public imagePreview: File;

  handleDrop(fileList: FileList) {

    let file = _.head(fileList);

    var reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onload = (event: any) => {
      this.imagePreview = event.target.result;
      this.model.image = event.target.result;
    }

  }


  startDateTime() {
    if (this.formStartDate && this.formStartTime) {
      this.model.startDateTime = format(new Date(this.formStartDate.year, this.formStartDate.month, this.formStartDate.day, this.formStartTime.hour, this.formStartTime.minute));
      this.generateAppointmentSlots();
    }
  }

  endDateTime() {
    if (this.formEndDate && this.formEndTime) {
      this.model.endDateTime = format(new Date(this.formEndDate.year, this.formEndDate.month, this.formEndDate.day, this.formEndTime.hour, this.formEndTime.minute));
      this.generateAppointmentSlots();
    }
  }

  setAddress(address) {
    this.zone.run(() => {
      this.model.address = address;
    });
  }
  constructor(private zone: NgZone, private eventService: EventsService) { }

  generateAppointmentSlots() {

    //Keep track of number of slots to generate, and the time offset
    let timeCount = 0;

    let numberOfAppointments = diffInMinutes(this.model.endDateTime, this.model.startDateTime) / this.timePerSlot;

    let appts = [];
    ////Iterate through each appointment, create the appropriate values then add them to the model. This is doing to much work. TODO - FIXME
    _.times(numberOfAppointments, () => {
      let appointment: Appointment = new Appointment();
      appointment.display = addMinutes(this.model.startDateTime, timeCount);
      appts.push(appointment);
      timeCount += this.timePerSlot;
    });

    this.model.appointments = appts;
    console.log(this.model);
  }

  onSubmit() {
    this.eventService.create(this.model).subscribe(res => console.log(res));
  }

  ngOnInit(): void {

  }
}
