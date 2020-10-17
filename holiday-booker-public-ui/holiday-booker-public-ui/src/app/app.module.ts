import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PublicUiComponent } from './public-ui/public-ui.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatSelectModule} from '@angular/material/select';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { SharedService } from 'src/Shared/shared.service';
import { MatSnackBarModule } from '@angular/material';
import { SharedModule } from 'src/Shared/shared.module';
import { EnquiryComponent } from './enquiry/enquiry.component';
import { NgxPageScrollCoreModule } from 'ngx-page-scroll-core';
import { AboutComponent } from './about/about.component';

@NgModule({
  declarations: [
    AppComponent,
    PublicUiComponent,
    AboutComponent,
    EnquiryComponent,
    AboutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgxDatatableModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSnackBarModule,
    MatSelectModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    NgxPageScrollCoreModule
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ],
  entryComponents: [
    EnquiryComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
