import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { StockadministrationRoutingModule } from './stockadministration-routing.module';
import { ManageStockComponent } from './manage-stock/manage-stock.component';
import { StockadministrationComponent } from './stockadministration.component';
import { ViewStockComponent } from './view-stock/view-stock.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule, MatAutocompleteModule, MatDialogModule } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { ViewduplicatesComponent } from './viewduplicates/viewduplicates.component';
import { UploadcsvComponent } from './uploadcsv/uploadcsv/uploadcsv.component';




@NgModule({
  imports: [
    CommonModule,
    StockadministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule,
    MatDialogModule
  ],
  declarations: [
    ManageStockComponent, 
    StockadministrationComponent,
    ViewStockComponent,
    ViewduplicatesComponent,
    UploadcsvComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ],
  entryComponents: [
    ViewduplicatesComponent,
    UploadcsvComponent
  ]
})
export class StockdministrationModule { }