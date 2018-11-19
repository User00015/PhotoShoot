import { Component, OnInit } from '@angular/core';
import { ImageService } from "../services/image.service";
import { BehaviorSubject, Subject } from "rxjs";
import { DomSanitizer } from "@angular/platform-browser"
import { Observable } from 'rxjs';
import { faAngleDown, faSpinner } from "@fortawesome/free-solid-svg-icons";

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  public images$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  faAngleDown = faAngleDown;
  faSpinner = faSpinner;

  currentPage: number = 0;
  sections: string[];
  currentSelection$: Subject<string> = new Subject<string>();
  currentSelection: string;
  loading: boolean = false;

  constructor(private imageService: ImageService, private sanitizer: DomSanitizer) {
  }

  onScroll() {
    this.loading = true;
    this.imageService.getGalleryImages(this.currentPage++, this.currentSelection).subscribe((data: any) => {
      let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
      this.images$.next([...this.images$.getValue(), ...sanitized]);
      this.loading = false;
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
      this.loading = true;
      this.imageService.getGalleryImages(this.currentPage, selection).subscribe((data: any) => {
        let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
        this.currentSelection = selection;
        this.images$.next(sanitized);
        this.loading = true;
      });

    });

  }

}
