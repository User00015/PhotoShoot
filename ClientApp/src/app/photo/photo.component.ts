import { Component, OnInit } from '@angular/core';
import { PhotoService } from "../services/photo.service";

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {

  private images: string[] = [];

  currentPage: number = 0;


  constructor(private photoService: PhotoService) {
  }

  onScroll() {
    this.photoService.getGalleryImages(this.currentPage).subscribe((data: any) => {
      this.images = data.concat(this.images);
    });

  }

  getGalleryPhotos() {
    return this.photoService.getGalleryImages(this.currentPage).subscribe((data: any) => {
      this.images = data;
    });
  }

  ngOnInit() {
    this.getGalleryPhotos();
  }

}
