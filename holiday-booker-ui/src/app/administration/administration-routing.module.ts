import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrationComponent } from './administration.component';
import { ManageProvidersComponent } from './manage-providers/manage-providers.component';

const routes: Routes = [{
  path: '',
  component: AdministrationComponent,
  children: [
    { path: '', redirectTo: 'manage-providers', pathMatch: 'full' },
    { path: 'manage-providers', component: ManageProvidersComponent, data: { breadcrumb: 'Manage Provider' } }
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrationRoutingModule { }
