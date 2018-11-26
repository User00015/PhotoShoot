import { Directive, ElementRef, OnInit, Output, EventEmitter, NgZone } from '@angular/core';
import { } from "googlemaps";
//declare var google: any;


@Directive({
  selector: '[google-place]'
})
export class GooglePlacesDirective implements OnInit {
  @Output() onSelect: EventEmitter<any> = new EventEmitter();
  private element: HTMLInputElement;

  constructor(elRef: ElementRef, private zone: NgZone) {
    //elRef will get a reference to the element where
    //the directive is placed
    this.element = elRef.nativeElement;

    const autocomplete = new google.maps.places.Autocomplete(this.element,
      {
        componentRestrictions: { country: 'us' }
      });
    //Event listener to monitor place changes in the input
    google.maps.event.addListener(autocomplete, 'place_changed', () => {
      //Emit the new address object for the updated place
      let place = autocomplete.getPlace();
      this.onSelect.emit(place.formatted_address);
    });
  }


  ngOnInit() {
  }

}
