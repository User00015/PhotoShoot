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
import { AboutComponent } from './about/about.component';
import { UploadComponent } from './upload/upload.component';
import { AdminComponent } from './admin/admin.component';

import { PhotoService } from "./services/photo.service";
import { UploadService } from './services/upload.service';
import { AuthGuardService } from "./services/auth-guard.service";

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
    UploadComponent,
    AdminComponent
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
      { path: 'about', component: AboutComponent },
      { path: 'admin', component: AdminComponent, canActivate: [AuthGuardService] }
    ])
  ],
  providers: [PhotoService, UploadService, AuthGuardService ],
  bootstrap: [AppComponent]
})
export class AppModule { }
