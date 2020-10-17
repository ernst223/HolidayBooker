import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageProvidersComponent } from './manage-providers/manage-providers.component';
import { AdministrationComponent } from './administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { SharedModule } from 'src/shared/shared.module';
import { SharedService } from 'src/shared/shared.serice';
import { MatSnackBarModule } from '@angular/material';
import { FormsModule } from '@angular/forms';



@NgModule({
  imports: [
    CommonModule,
    AdministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule
  ],
  declarations: [
    ManageProvidersComponent, 
    AdministrationComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class AdministrationModule { }
