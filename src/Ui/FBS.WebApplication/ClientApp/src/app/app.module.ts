import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import {Md5} from 'ts-md5/dist/md5';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FlightOverviewComponent } from './flight/flight-overview.component';
import { environment } from 'src/environments/environment';
import { BOOKING_WRITE_API_BASE_URL } from './services/booking-write.service';
import { BOOKING_READ_API_BASE_URL } from './services/booking-read.service';
import { FLIGHTCONTROL_API_BASE_URL } from './services/flightcontrol.service';
import { CUSTOMER_API_BASE_URL } from './services/customer.service';
import { AUTHORIZATION_API_BASE_URL } from './services/authorization.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';
import { FlightDetailComponent } from './flight/flight-detail.component';
import { LoginComponent } from './login/login.component';
import { AuthorizedService } from './services/authorized.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FlightOverviewComponent,
    FlightDetailComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'flight/:id', component: FlightDetailComponent },
      { path: 'login', component: LoginComponent },
      { path: '', component: FlightOverviewComponent, pathMatch: 'full' }
    ]),
    BrowserAnimationsModule,
    FlexLayoutModule,
    MaterialModule,
    ReactiveFormsModule
  ],
  providers:  [
    { provide: BOOKING_WRITE_API_BASE_URL, useValue: environment.bookingWriteApiBaseUrl },
    { provide: BOOKING_READ_API_BASE_URL, useValue: environment.bookingReadApiBaseUrl },
    { provide: FLIGHTCONTROL_API_BASE_URL, useValue: environment.flightcontrolApiBaseUrl },
    { provide: CUSTOMER_API_BASE_URL, useValue: environment.customerApiBaseUrl },
    { provide: AUTHORIZATION_API_BASE_URL, useValue: environment.authorizationApiBaseUrl },
    Md5,
    AuthorizedService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
