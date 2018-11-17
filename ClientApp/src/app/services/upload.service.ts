import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent, HttpParams } from "@angular/common/http";

@Injectable()
export class UploadService {
  private imagesUrl: string = "https://localhost:5001/api/Images/uploadImages";
  private imageTypesUrl: string = "https://localhost:5001/api/Data/imageTypes";

  constructor(private http: HttpClient) { }

  getGalleryImages() {
    return this.http.get(this.imagesUrl);
  }

  uploadImages(images: FormData, imageType: string) {
    return this.http.post(this.imagesUrl, images, { params: { "type": imageType}, reportProgress: true, observe: 'events' });
  }


  getImageTypes() {
    return this.http.get(this.imageTypesUrl);
  }
}

