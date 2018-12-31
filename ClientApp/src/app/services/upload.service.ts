import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent, HttpParams } from "@angular/common/http";
import {environment} from "../../environments/environment";

@Injectable()
export class UploadService {
  private baseUrl = environment.baseUrl;
  private imagesUrl: string = this.baseUrl + "api/Images/uploadImages";
  private imageTypesUrl: string = this.baseUrl + "api/Data/imageTypes";

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

