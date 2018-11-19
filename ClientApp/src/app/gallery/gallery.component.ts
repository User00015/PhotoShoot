import { Component, OnInit } from '@angular/core';
import { ImageService } from "../services/image.service";
import { BehaviorSubject, Subject } from "rxjs";
import { DomSanitizer } from "@angular/platform-browser"
import { Observable } from 'rxjs';
import {faAngleDown} from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  public images$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  faAngleDown = faAngleDown;

  currentPage: number = 0;
  sections: string[];
  currentSelection$: Subject<string> = new Subject<string>();
  currentSelection: string;

  constructor(private imageService: ImageService, private sanitizer: DomSanitizer) {
  }

  onScroll() {
    this.imageService.getGalleryImages(this.currentPage++, this.currentSelection).subscribe((data: any) => {
      let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
      this.images$.next([...this.images$.getValue(), ...sanitized]);
    });

  }

  onSelection(event) {
    this.currentPage = 0;
    this.currentSelection$.next(event);
  }


  ngOnInit() {
    this.imageService.getImageTypes().subscribe((types: string[]) => {
      this.sections = types;
    });

    this.currentSelection$.subscribe(selection => {
      this.imageService.getGalleryImages(this.currentPage, selection).subscribe((data: any) => {
        let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
        this.currentSelection = selection;
        this.images$.next(sanitized);
      });

    });

  }

}
