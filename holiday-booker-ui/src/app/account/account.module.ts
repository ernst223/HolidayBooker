import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from './account.service';
import { MatSnackBarModule } from '@angular/material';
import { SharedModule } from 'src/shared/shared.module';
import { LoginComponent } from './login/login.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    RouterModule,
    MatSnackBarModule
  ],
  declarations: [LoginComponent],
  providers: [AccountService]
})
export class AccountModule { }
