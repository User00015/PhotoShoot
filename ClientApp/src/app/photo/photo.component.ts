import { Component, OnInit } from '@angular/core';
import { PhotoService } from "../services/photo.service";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.scss']
})
export class PhotoComponent implements OnInit {

  private images: string[] = [];
  private images$: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

  currentPage: number = 0;


  constructor(private photoService: PhotoService) {
  }

  onScroll() {
    this.photoService.getGalleryImages(this.currentPage++).subscribe((data: any) => {
      this.images$.next([...this.images$.getValue(), ...data]);
    });

  }

  getGalleryPhotos() {
    return this.photoService.getGalleryImages(this.currentPage).subscribe((data: any) => {
      this.images$.next(data);
    });
  }

  ngOnInit() {
    this.getGalleryPhotos();
  }

}
