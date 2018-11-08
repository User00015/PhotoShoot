import { Component, OnInit } from '@angular/core';
import { PhotoService } from "../services/photo.service";
import { tap, concat } from 'rxjs/operators';

@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {

  private images: string[];

  currentPage: number = 1;


  constructor(private photoService: PhotoService) {
  }

  clicked() {
    this.nextPage();
  }

  getGalleryPhotos() {
    console.log("hello");
    return this.photoService.getGalleryImages(this.currentPage).subscribe((data: any) => {
      this.images = data;
    });
  }

  private nextPage() {
    this.currentPage++;
    this.getGalleryPhotos();
  }

  ngOnInit() {
    this.getGalleryPhotos();
  }

}
