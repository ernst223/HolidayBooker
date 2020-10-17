import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShellComponent } from './shell/shell.component';
import { AdministrationModule } from './administration/administration.module';
import { ResortdministrationModule } from './resortadministration/resortadministration.module';
import { StockdministrationModule } from './stockadministration/stockadministration.module';
import { DatasetadministrationModule } from './datasetadministration/datasetadministration.module';



const routes: Routes = [
  // { path: '', redirectTo: '/account/login', pathMatch: 'full' },
  {
    path: '',
     canActivate: [],
    component: ShellComponent,
    children: [
      { path: '', redirectTo: '/account/login', pathMatch: 'full' },
      { path: 'administration', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'resortadministration', loadChildren: './resortadministration/resortadministration.module#ResortdministrationModule' },
      { path: 'stockadministration', loadChildren: './stockadministration/stockadministration.module#StockdministrationModule'},
      { path: 'datasetadministration', loadChildren: './datasetadministration/datasetadministration.module#DatasetadministrationModule'},
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
