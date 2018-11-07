import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from '../services/upload.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  onFileSelected(event) {
    this.uploadService.uploadGalleryImages(event.target.files).subscribe(p => console.log(p));
  };

  constructor(private uploadService: UploadService) { }

  ngOnInit() {
  }

}
