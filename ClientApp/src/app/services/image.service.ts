import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {DomSanitizer} from "@angular/platform-browser"
import {Image} from "../Models/image-model";
import {environment} from "../../environments/environment";

@Injectable()
export class ImageService {

  private baseUrl = environment.baseUrl;
  private bannerUrl: string = this.baseUrl + "api/Images/banner";
  private galleryUrl: string =  this.baseUrl +"api/Images/getImages";
  private imagesUrl: string =  this.baseUrl +"api/Images/uploadImages";
  private imageTypesUrl: string =  this.baseUrl +"api/Data/imageTypes";
  private galleryDeleteUrl: string =  this.baseUrl +"api/Images/deleteImage";

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
