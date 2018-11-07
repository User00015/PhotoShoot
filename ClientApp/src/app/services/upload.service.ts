import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class UploadService {

  private url: string = "https://localhost:5001/api/Images/gallery";
  private httpOptions = {
    
    headers: new HttpHeaders({
      //'Accept': "multipart/form-data"
    })
  };

  constructor(private http: HttpClient) { }

  getGalleryImages() {
    return this.http.get(this.url);
  }

  uploadGalleryImages(images: FileList) {
    let formData: FormData = new FormData();
    for (let i = 0; i < images.length; i++) {
    formData.append('file', images[i], images[i].name);
    };
    //formData.append('images', images);
    console.log(formData);
    console.log(this.httpOptions);
    return this.http.post(this.url, formData);
  }
}

