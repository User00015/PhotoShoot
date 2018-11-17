import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {DomSanitizer} from "@angular/platform-browser"

@Injectable()
export class PhotoService {

  private bannerUrl: string = "https://localhost:5001/api/Images/banner";
  private galleryUrl: string = "https://localhost:5001/api/Images/getImages";

  constructor(private http: HttpClient) { }

  
  private httpOptions = {
    params: {}
  };

  getBannerImages(): Observable<string> {
    return this.http.get<string>(this.bannerUrl);
  }

  getGalleryImages(size: number): Observable<string> {
    this.httpOptions.params = { "size": size, "type": "Banner" };
    return this.http.get<string>(this.galleryUrl, this.httpOptions);
  }

}
