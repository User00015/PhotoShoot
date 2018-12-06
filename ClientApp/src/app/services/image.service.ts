import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {DomSanitizer} from "@angular/platform-browser"
import {Image} from "../Models/image-model";

@Injectable()
export class ImageService {

  private bannerUrl: string = "https://localhost:5001/api/Images/banner";
  private galleryUrl: string = "https://localhost:5001/api/Images/getImages";

  private imagesUrl: string = "https://localhost:5001/api/Images/uploadImages";
  private imageTypesUrl: string = "https://localhost:5001/api/Data/imageTypes";

  private galleryDeleteUrl: string = "https://localhost:5001/api/Images/deleteImage";

  constructor(private http: HttpClient) { }
  
  private httpOptions = {
    params: {}
  };

  getBannerImages(): Observable<string> {
    return this.http.get<string>(this.bannerUrl);
  }

  getGalleryImages(size: number, type: string): Observable<Image> {
    this.httpOptions.params = { "size": size, "type": type };
    return this.http.get<Image>(this.galleryUrl, this.httpOptions);
  }

  uploadImages(images: FormData, imageType: string) {
    return this.http.post(this.imagesUrl, images, { params: { "type": imageType}, reportProgress: true, observe: 'events' });
  }

  getImageTypes() {
    return this.http.get(this.imageTypesUrl);
  }

  deleteImage(id: string) {
    return this.http.delete(this.galleryDeleteUrl, { params: { "id": id } });

  }
}
