import { Component, OnInit } from '@angular/core';
import {HttpEvent} from '@angular/common/http';
import { UploadService } from '../services/upload.service';
import * as _ from 'lodash';
import { faFileUpload, faFolderOpen } from "@fortawesome/free-solid-svg-icons";


@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

  faFileUpload = faFileUpload;
  faFolderOpen = faFolderOpen;
  dropzoneActive: boolean = false;
  formData: FormData = new FormData();

  public imagesUrl: string[] = [];
  dropzoneState($event: boolean) {
    this.dropzoneActive = $event;

  }

  handleDrop(fileList: FileList) {
    _.map(fileList, file => {

      var reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = (event: any) => {
        this.imagesUrl = _.concat(this.imagesUrl, event.target.result);
      }

      this.formData.append('files', file, file.name);
    });
  }

  upload() {
    this.uploadService.uploadGalleryImages(this.formData);
  }



  constructor(private uploadService: UploadService) { }

  ngOnInit() {
  }

}
