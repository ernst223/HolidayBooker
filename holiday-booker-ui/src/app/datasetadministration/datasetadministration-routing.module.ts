import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DatasetadministrationComponent } from './datasetadministration.component';
import { ManageDatasetComponent } from './manage-dataset/manage-dataset.component';


const routes: Routes = [{
  path: '',
  component: DatasetadministrationComponent,
  children: [
    { path: '', redirectTo: 'manage-dataset', pathMatch: 'full' },
    { path: 'manage-dataset', component: ManageDatasetComponent, data: { breadcrumb: 'Manage Dataset' } }
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DatasetadministrationRoutingModule { }