import { Component, OnInit } from '@angular/core';
import { faPhone, } from '@fortawesome/free-solid-svg-icons';
import { faFacebookMessenger } from '@fortawesome/free-brands-svg-icons';
import { faEnvelope } from '@fortawesome/free-regular-svg-icons'

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  faPhone = faPhone;
  faFacebookMessenger = faFacebookMessenger;
  faEnvelope = faEnvelope;

  constructor() { }

  private aboutImage = "assets/images/about.jpg";

  ngOnInit() {
  }

}
