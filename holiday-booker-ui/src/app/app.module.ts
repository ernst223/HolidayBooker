import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ShellBreadcrumbsComponent } from './shell/shell-breadcrumbs/shell-breadcrumbs.component';
import { ShellNavListComponent } from './shell/shell-nav-list/shell-nav-list.component';
import { ShellComponent } from './shell/shell.component';
import { SharedModule } from '../shared/shared.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { MenuService } from './shell/menu.service';
import { PublicUiModule } from './public-ui/public-ui.module';
import { AccountModule } from './account/account.module';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HeaderInterceptor } from './interceptors/header.interceptor';


@NgModule({
  declarations: [
    AppComponent,
    ShellBreadcrumbsComponent,
    ShellNavListComponent,
    ShellComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    BrowserAnimationsModule,
    PublicUiModule,
    AccountModule
  ],
  providers: [
    MenuService,
    { provide: HTTP_INTERCEPTORS, useClass: HeaderInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(router: Router) {
  }
}
