import { Component } from '@angular/core';
import { faHome, faImage, faCalendarAlt, faDirections } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent {
  faHome = faHome;
  faImage = faImage;
  faCalendarAlt = faCalendarAlt;
  faDirections = faDirections;
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
