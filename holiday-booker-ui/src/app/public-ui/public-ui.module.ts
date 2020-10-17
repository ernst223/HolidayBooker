import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { HolidaysComponent } from './holidays/holidays.component';
import { PublicUiRoutingModule } from './public-ui-routing.module';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    PublicUiRoutingModule,
    MatSnackBarModule,
    ReactiveFormsModule,
  ],
  declarations: [
    HolidaysComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class PublicUiModule { }
