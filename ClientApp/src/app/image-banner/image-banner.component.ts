import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PhotoService } from "../services/photo.service";
import {DomSanitizer} from "@angular/platform-browser"

@Component({
  selector: 'app-image-banner',
  templateUrl: './image-banner.component.html',
  styleUrls: ['./image-banner.component.scss']
})

export class ImageBannerComponent implements OnInit {

  public fadeIn = 'fade-in';
  

  private imgRotation: any;


  constructor(private router: Router, private photoService: PhotoService, private sanitizer: DomSanitizer) {
    photoService.getBannerImages().subscribe((data: any) => {
      this.imgRotation = data.map(url => this.sanitizer.bypassSecurityTrustUrl(url));
    });
  }

  ngOnInit() {
  }

  showBanner() {
    return this.router.url == '/' || this.router.url == '/home';
  }

}
