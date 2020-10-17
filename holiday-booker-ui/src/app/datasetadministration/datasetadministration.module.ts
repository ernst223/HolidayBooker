import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { DatasetadministrationRoutingModule } from './datasetadministration-routing.module';
import { ManageDatasetComponent } from './manage-dataset/manage-dataset.component';
import { DatasetadministrationComponent } from './datasetadministration.component';
import { ManageDatasetRegionComponent } from './manage-dataset/manage-dataset-region/manage-dataset-region.component';
import { ManageDatasetCountryComponent } from './manage-dataset/manage-dataset-country/manage-dataset-country.component';
import { ManageDatasetUnitsizeComponent } from './manage-dataset/manage-dataset-unitsize/manage-dataset-unitsize.component';
import { SharedService } from 'src/shared/shared.serice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ManageDatasetAreComponent } from './manage-dataset/manage-dataset-area/manage-dataset-are.component';



@NgModule({
  imports: [
    CommonModule,
    DatasetadministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule
  ],
  declarations: [
    ManageDatasetComponent,
    DatasetadministrationComponent,
    ManageDatasetRegionComponent,
    ManageDatasetCountryComponent,
    ManageDatasetUnitsizeComponent,
    ManageDatasetAreComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]

})
export class DatasetadministrationModule {}
