import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";


import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { GalleryComponent } from './gallery/gallery.component';
import { ImageBannerComponent } from './image-banner/image-banner.component';
import { EventsComponent } from './events/events.component';
import { FooterComponent } from './footer/footer.component';
import { AboutComponent } from './about/about.component';
import { UploadComponent } from './upload/upload.component';
import { AdminComponent } from './admin/admin.component';
import { LoginComponent } from './login/login.component';
import { ImageComponent } from './gallery/image/image.component';
import { EventsListComponent } from './events/events-list/events-list.component';
import { CreateEventComponent } from './events/create/create-event.component';

import { ImageService } from "./services/image.service";
import { AuthGuardService } from "./services/auth-guard.service";
import { AuthService } from "./services/auth.service";
import { EventsService } from "./services/events.service";

import { JwtInterceptor } from "./Interceptor/jwt.interceptor";
import { FileDropDirective } from './Directives/file-drop.directive';
import { GooglePlacesDirective } from './directives/google-place.directive';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    GalleryComponent,
    ImageBannerComponent,
    EventsComponent,
    FooterComponent,
    AboutComponent,
    UploadComponent,
    AdminComponent,
    LoginComponent,
    FileDropDirective,
    ImageComponent,
    EventsListComponent,
    CreateEventComponent,
    GooglePlacesDirective

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    FontAwesomeModule,
    InfiniteScrollModule,
    NgbModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'gallery', component: GalleryComponent },
      {
        path: 'events', component: EventsComponent, children: [
          { path: '', component: EventsListComponent },
          { path: 'create', component: CreateEventComponent }
        ]
      },
      { path: 'about', component: AboutComponent },
      { path: 'admin', component: AdminComponent, canActivate: [AuthGuardService] },
      { path: 'login', component: LoginComponent },
      { path: '**', component: HomeComponent }

    ])
  ],
  providers: [ImageService, AuthGuardService, AuthService, EventsService, { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
