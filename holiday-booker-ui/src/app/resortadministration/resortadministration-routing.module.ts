import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ResortadministrationComponent } from './resortadministration.component';
import { ManageResortsComponent } from './manage-resorts/manage-resorts.component';


const routes: Routes = [{
  path: '',
  component: ResortadministrationComponent,
  children: [
    { path: '', redirectTo: 'manage-resort', pathMatch: 'full' },
    { path: 'manage-resort', component: ManageResortsComponent, data: { breadcrumb: 'Manage Resort' } }
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResortadministrationRoutingModule { }