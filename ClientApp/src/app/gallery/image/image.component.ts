import { Component, OnInit, Output, Input, EventEmitter, HostListener } from '@angular/core';
import { faTrash } from "@fortawesome/free-solid-svg-icons";
import { Image } from "../../Models/image-model";
import { ImageService } from "../../services/image.service";
import { AuthService } from "../../services/auth.service";


@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit {

  hovered: boolean = false;
  exists: boolean = true;
  @Input() imgSrc: Image;
  faTrash = faTrash;
  constructor(private imgService: ImageService, private authService: AuthService) { }

  @HostListener('mouseenter', ['$event']) onMouseEnter($event) {
    $event.preventDefault();
    this.hovered = true;
  }

  onDelete(event) {
    event.preventDefault();
    this.imgService.deleteImage(this.imgSrc.id).subscribe(() => {
      this.exists = false;
    });
  }

  @HostListener('mouseleave', ['$event']) onMouseLeave($event) {
    $event.preventDefault();
    this.hovered = false;

  }
  ngOnInit() {
  }

}
