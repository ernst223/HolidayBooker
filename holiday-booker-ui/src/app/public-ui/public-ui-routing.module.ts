import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HolidaysComponent } from './holidays/holidays.component';


const routes: Routes = [{
  path: '',
  children: [
    { path: '', redirectTo: 'holidays', pathMatch: 'full' },
    { path: 'holidays', component: HolidaysComponent}
  ],
  data: { breadcrumb: 'Public' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicUiRoutingModule { }