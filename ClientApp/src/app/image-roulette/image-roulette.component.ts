import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouletteService } from "../services/roulette.service";

@Component({
  selector: 'app-image-roulette',
  templateUrl: './image-roulette.component.html',
  styleUrls: ['./image-roulette.component.scss']
})

export class ImageRouletteComponent implements OnInit {

  public fadeIn = 'fade-in';

  private imgUrls = [
      {
        url: 'assets/images/1.jpg'
      },
      {
        url: 'assets/images/2.jpg'
      },
      {
        url: 'assets/images/3.jpg'
      },
      {
        url: 'assets/images/4.jpg'
      }
    ];

  constructor(private router: Router, private rouletteService: RouletteService) {
    rouletteService.getRouletteImages().subscribe(data => console.log(data));
  }

  ngOnInit() {
  }

}
