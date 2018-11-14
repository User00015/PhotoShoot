import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from '../services/upload.service';
import {Observable, BehaviorSubject} from "rxjs";
import * as _ from 'lodash';


@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {

private testUrl = "../assets/images/test.jpg";
private imagesUrl: string[] = [];

  onBrowseFiles() {
    this.imagesUrl = [];
  }

  upload() {
    //this.uploadService.uploadGalleryImages(event.target.files).subscribe(p => console.log(p));
  }

  onFileSelected(event) {

        _.map(event.target.files, file => {

        var reader = new FileReader();
        reader.readAsDataURL(file);

        reader.onload = event => {
          this.imagesUrl = _.concat(this.imagesUrl, event.target.result);
        }
    });

  };


  constructor(private uploadService: UploadService) { }

  ngOnInit() {
    //this.imagesToUpload.subscribe(p => console.log(p));
  }

}
