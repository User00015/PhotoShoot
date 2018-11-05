import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Injectable()
export class RouletteService {

  private url: string = "https://localhost:5001/api/Images/roulette";

  constructor(private http: HttpClient) { }

  getRouletteImages() {
    return this.http.get(this.url);
  }

}
