import { Component, OnInit } from '@angular/core';
import { PhotoService } from "../services/photo.service";
import { BehaviorSubject } from "rxjs/BehaviorSubject";
import {DomSanitizer} from "@angular/platform-browser"

@Component({
  selector: 'app-gallery',
  templateUrl: './gallery.component.html',
  styleUrls: ['./gallery.component.scss']
})
export class GalleryComponent implements OnInit {

  public images$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  currentPage: number = 0;


  constructor(private photoService: PhotoService, private sanitizer: DomSanitizer) {
  }

  onScroll() {
    this.photoService.getGalleryImages(this.currentPage++).subscribe((data: any) => {
      let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
      this.images$.next([...this.images$.getValue(), ...sanitized]);
    });

  }

  getGalleryPhotos() {
    return this.photoService.getGalleryImages(this.currentPage).subscribe((data: any) => {
      let sanitized = data.map(p => this.sanitizer.bypassSecurityTrustUrl(p));
      this.images$.next(sanitized);
    });
  }

  ngOnInit() {
    this.getGalleryPhotos();
    this.uploadService.getImageTypes().subscribe((types: string[]) => {
      this.uploadSections = types;
    });
  }

}
