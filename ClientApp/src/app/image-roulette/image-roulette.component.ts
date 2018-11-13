import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PhotoService } from "../services/photo.service";
import { IFileModel } from "../Models/file-model";
import {DomSanitizer} from "@angular/platform-browser"

@Component({
  selector: 'app-image-roulette',
  templateUrl: './image-roulette.component.html',
  styleUrls: ['./image-roulette.component.scss']
})

export class ImageRouletteComponent implements OnInit {

  public fadeIn = 'fade-in';
  

  private imgRotation: any;


  constructor(private router: Router, private photoService: PhotoService, private sanitizer: DomSanitizer) {
    photoService.getRouletteImages().subscribe((data: any) => {
      this.imgRotation = data.map(url => this.sanitizer.bypassSecurityTrustUrl(url));
    });
  }

  ngOnInit() {
  }

  showRoulette() {
    return this.router.url == '/' || this.router.url == '/home';
  }

}
