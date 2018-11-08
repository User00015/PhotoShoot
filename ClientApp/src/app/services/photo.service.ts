import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import {Observable} from "rxjs/Observable";

@Injectable()
export class PhotoService {

  private rouletteUrl: string = "https://localhost:5001/api/Images/roulette";
  private galleryUrl: string = "https://localhost:5001/api/Images/gallery";

  constructor(private http: HttpClient) { }

  
  private httpOptions = {
    params: {}
  };

  getRouletteImages() {
    return this.http.get(this.rouletteUrl);
  }

  getGalleryImages(size: number): Observable<string> {
    this.httpOptions.params = { "size": size };
    return this.http.get<string>(this.galleryUrl, this.httpOptions);
  }

}
