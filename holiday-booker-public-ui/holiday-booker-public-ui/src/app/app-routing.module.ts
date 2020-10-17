import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { PublicUiComponent } from './public-ui/public-ui.component';
import { AboutComponent } from './about/about.component';


const routes: Routes = [
  {
    path: '',
     canActivate: [],
    component: AppComponent,
    children: [
      { path: '', redirectTo: 'holidays', pathMatch: 'full' },
      {path: 'holidays', component: PublicUiComponent},
      {path: 'about', component: AboutComponent}
     ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
