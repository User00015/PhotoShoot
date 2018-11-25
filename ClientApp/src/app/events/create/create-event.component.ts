import { Component, OnInit, NgZone } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Event } from "../../Models/event-model";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import * as _ from 'lodash';

@Component({
  selector: 'app-create',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent implements OnInit {
  ngOnInit(): void { }

  public model: Event = new Event();
  formData: FormData = new FormData();

  public faCalendar = faCalendar;
  public imagePreview: File;

  closeResult: string;

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
  constructor(private activeModal: NgbActiveModal, private zone: NgZone) { }


}
