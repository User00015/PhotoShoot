import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { PhotoComponent } from './photo/photo.component';
import { ImageRouletteComponent } from './image-roulette/image-roulette.component';
import { EventsComponent } from './events/events.component';
import { FooterComponent } from './footer/footer.component';

import { PhotoService } from "./services/photo.service";
import { AboutComponent } from './about/about.component';
import { UploadComponent } from './upload/upload.component';
import { UploadService } from './services/upload.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PhotoComponent,
    ImageRouletteComponent,
    EventsComponent,
    FooterComponent,
    AboutComponent,
    UploadComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FontAwesomeModule,
    InfiniteScrollModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'photos', component: PhotoComponent },
      { path: 'events', component: EventsComponent },
      { path: 'about', component: AboutComponent }
    ])
  ],
  providers: [PhotoService, UploadService],
  bootstrap: [AppComponent]
})
export class AppModule { }
