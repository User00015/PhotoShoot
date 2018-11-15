import { Component, OnInit, ViewChild } from '@angular/core';
import { UploadService } from '../services/upload.service';
import { Upload } from '../Models/file-model'
import { Observable, BehaviorSubject } from "rxjs";
import * as _ from 'lodash';
import { faFileUpload, faCircle  } from "@fortawesome/free-solid-svg-icons";


@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  faFileUpload   = faFileUpload;
  faCircle = faCircle;
  currentUpload: Upload;
  dropzoneActive: boolean = false;

  public imagesUrl: string[] = [];
  dropzoneState($event: boolean) {
    this.dropzoneActive = $event;

  }

  handleDrop(fileList: FileList) {
    let filesIndex = _.range(fileList.length);
    //_.each(filesIndex, (idx) => {
    //    this.currentUpload = new Upload(fileList[idx]);
    //    console.log("here's where I upload the file to the service", this.currentUpload);
    //  }
    _.map(fileList, file => {

      var reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = (event: any) => {
        this.imagesUrl = _.concat(this.imagesUrl, event.target.result);
      }
    });
  }

  //public testUrl = "../assets/images/test.jpg";

  //onBrowseFiles() {
  //  this.imagesUrl = [];
  //}

  //  upload() {
  //    //this.uploadService.uploadGalleryImages(event.target.files).subscribe(p => console.log(p));
  //  }

  //onFileSelected(event) {

  //  _.map(event.target.files, file => {

  //    var reader = new FileReader();
  //    reader.readAsDataURL(file);

  //    reader.onload = (event: any) => {
  //      this.imagesUrl = _.concat(this.imagesUrl, event.target.result);
  //    }
  //  });

  //};


  constructor(private uploadService: UploadService) { }

  ngOnInit() {
    //this.imagesToUpload.subscribe(p => console.log(p));
  }

}
