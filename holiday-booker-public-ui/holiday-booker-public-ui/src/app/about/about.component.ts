import { Component, OnInit, Inject, ElementRef, ViewChild } from '@angular/core';
import { PageScrollService } from 'ngx-page-scroll-core';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog, MatSnackBar } from '@angular/material';
import { SharedService } from 'src/Shared/shared.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
  SubmitEmailValue: string;
  @ViewChild('ContactSection', { static: false }) public contactStructure:ElementRef;
  constructor(private pageScrollService: PageScrollService, @Inject(DOCUMENT) private document: any
  , private router: Router, private dialog: MatDialog, private service: SharedService, private _snackBar: MatSnackBar) { }

  ngOnInit() {
  }
  submitEmail() {
    this.service.addNewEmail(this.SubmitEmailValue).subscribe(a => {
      this.openSnackBar('Successful', 'Close');
    });
  }

  public moveToContact():void {
    this.contactStructure.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 4000,
    });
  }
}
