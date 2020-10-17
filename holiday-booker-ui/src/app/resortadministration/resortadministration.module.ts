import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { ResortadministrationRoutingModule } from './resortadministration-routing.module';
import { ManageResortsComponent } from './manage-resorts/manage-resorts.component';
import { ResortadministrationComponent } from './resortadministration.component';
import { ViewResortComponent } from './view-resort/view-resort.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';



@NgModule({
  imports: [
    CommonModule,
    ResortadministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule
  ],
  declarations: [
    ManageResortsComponent,
    ResortadministrationComponent, ViewResortComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class ResortdministrationModule { }