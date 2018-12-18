import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../services/events.service';
import { Observable } from 'rxjs';
import { faSpinner } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-checkout-confirmation',
  templateUrl: './checkout-confirmation.component.html',
  styleUrls: ['./checkout-confirmation.component.css']
})
export class CheckoutConfirmationComponent implements OnInit {

  constructor(private route: ActivatedRoute, private eventService: EventsService) { }

  faSpinner = faSpinner;
  public isPaid$ = new Observable<boolean>();

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        this.isPaid$ = this.eventService.confirmAppointment(params.transactionId, params.referenceId);
      });
  }

}
