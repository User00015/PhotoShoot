import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-checkout-confirmation',
  templateUrl: './checkout-confirmation.component.html',
  styleUrls: ['./checkout-confirmation.component.css']
})
export class CheckoutConfirmationComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  public confirmation: any;

  ngOnInit() {
    this.route.queryParams
      .subscribe(params => {
        console.log(params);
        this.confirmation = params;
      });
  }

}
