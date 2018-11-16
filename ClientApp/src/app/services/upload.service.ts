import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent } from "@angular/common/http";

@Injectable()
export class UploadService {
  private galleryUrl: string = "https://localhost:5001/api/Images/uploadGallery";
  private imageTypesUrl: string = "https://localhost:5001/api/Data/imageTypes";

  constructor(private http: HttpClient) { }

  getGalleryImages() {
    return this.http.get(this.galleryUrl);
  }

  uploadGalleryImages(images: FormData, imageType: string) {
    return this.http.post(`${this.galleryUrl}?type=${imageType.trim()}`, images, { reportProgress: true, observe: 'events' });
  }


  getImageTypes() {
    return this.http.get(this.imageTypesUrl);
  }
}

