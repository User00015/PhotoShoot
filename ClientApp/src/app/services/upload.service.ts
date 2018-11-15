import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpEvent } from "@angular/common/http";

@Injectable()
export class UploadService {
  private url: string = "https://localhost:5001/api/Images/uploadGallery";

  constructor(private http: HttpClient) { }

  getGalleryImages() {
    return this.http.get(this.url);
  }

  uploadGalleryImages(images: FormData) {
    return this.http.post(this.url, images, { reportProgress: true, observe: 'events' }).subscribe((event:
      HttpEvent<any>) => {
      switch (event.type) {
        case 1:
          {
            console.log(event['loaded'] / event['total'] * 100);
          }
          break;
      }

    });
  }


}

