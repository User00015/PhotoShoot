import { Component, OnInit } from '@angular/core';
import { HttpEvent } from '@angular/common/http';
import { ImageService } from '../services/image.service';
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
  progress: number = 0;
  public uploadSections: string[];
  public selectedSection: string;
  public fileList;

  public imagesUrl: string[] = [];
  dropzoneState($event: boolean) {
    this.dropzoneActive = $event;

  }

  handleDrop(fileList: FileList) {
    this.progress = 0;
    _.map(fileList, file => {

      var reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = (event: any) => {
        this.imagesUrl = _.concat(this.imagesUrl, event.target.result);
      }

      this.formData.append('files', file, file.name);
    });
  }

  upload(event) {
    this.imageService.uploadImages(this.formData, this.selectedSection).subscribe((event:
      HttpEvent<any>) => {
      switch (event.type) {
        case 1:
          {
            this.progress = Math.round(event['loaded'] / event['total'] * 100);
          }
          break;
      }
    },
      error => { console.log(error) },
      () => {
        this.imagesUrl = [];
        this.selectedSection = undefined;
        this.formData = new FormData();
      });
  }

  onSelection(selection) {
    this.selectedSection = selection;
  }


  constructor(private imageService: ImageService) { }

  ngOnInit() {
    this.imageService.getImageTypes().subscribe((types: string[]) => {
      this.uploadSections = types;
    });
  }

}
