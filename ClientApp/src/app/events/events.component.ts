import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreateEventComponent } from "./create/create-event.component";

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  constructor(private modalService: NgbModal) { }
  openCreateModal() {
    const modalRef = this.modalService.open(CreateEventComponent);
    modalRef.componentInstance.name = "World";

    modalRef.result.then((result) => console.log("result", result)).catch((error) => {
      console.log("error", error);
    });

  }
  ngOnInit() {
  }

}
