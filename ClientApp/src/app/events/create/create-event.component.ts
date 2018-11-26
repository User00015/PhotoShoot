import { Component, OnInit, NgZone } from '@angular/core';
import { Event } from "../../Models/event-model";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import * as _ from 'lodash';
var diffInMinutes = require('date-fns/difference_in_minutes');
var addMinutes = require('date-fns/add_minutes');

import * as _ from 'lodash';

@Component({
  selector: 'app-create',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
  ngOnInit(): void {
    this.model.startTime = {hour: 18, minute: 0}
    this.model.endTime = {hour: 20, minute: 0}
  }

  public model: Event = new Event();
  formData: FormData = new FormData();

  public appointmentTimes: string[] = [];

  public faCalendar = faCalendar;
  public imagePreview: File;

  closeResult: string;

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
  constructor(private zone: NgZone) { }

previewAppointmentSlots() {
  this.appointmentTimes = [];
  let foo = this.timePerSlot;
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

    _.times(numberOfAppointments, () => {
      this.appointmentTimes.push(addMinutes(startDate, foo));
      foo += this.timePerSlot;
    } );

  console.log(this.appointmentTimes);


}

  onSubmit(form) {
    console.log(form);
  }


}
