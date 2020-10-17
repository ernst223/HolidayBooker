import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StockadministrationComponent } from './stockadministration.component';
import { ManageStockComponent } from './manage-stock/manage-stock.component';



const routes: Routes = [{
  path: '',
  component: StockadministrationComponent,
  children: [
    { path: '', redirectTo: 'manage-stock', pathMatch: 'full' },
    { path: 'manage-stock', component: ManageStockComponent, data: { breadcrumb: 'Manage Stock' } }
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StockadministrationRoutingModule { }