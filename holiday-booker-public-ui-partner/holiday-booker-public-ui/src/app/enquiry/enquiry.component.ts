import { Component, OnInit, Inject, ElementRef } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatSnackBar } from '@angular/material';
import { SharedService } from 'src/Shared/shared.service';
import { EnquiryDto } from 'src/Shared/shared.models';

@Component({
  selector: 'app-enquiry',
  templateUrl: './enquiry.component.html',
  styleUrls: ['./enquiry.component.scss']
})
export class EnquiryComponent implements OnInit {

  nameValue: string;
  lastnameValue: string;
  emailValue: string;
  cellValue: string;
  dobValue: string;
  adultsValue: number;
  under12Value: number;
  note = '';

  constructor( private _snackBar: MatSnackBar, private dialogRef: MatDialogRef<EnquiryComponent>
    , @Inject(MAT_DIALOG_DATA) public data: any, private service: SharedService) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close();
  }
  submit() {
    console.log("Enquiring for Holdiday Id: " + this.data.Id);

    if (this.nameValue != undefined) {
      if (this.lastnameValue != undefined) {
        if (this.emailValue != undefined) {
          if (this.cellValue != undefined) {
            if (this.dobValue != undefined) {
              this.dobValue = this.dobValue.split(' ').join('-');
              this.dobValue = this.dobValue.split('/').join('-');
              const enquiryData: EnquiryDto = {
                name : this.nameValue,
                lastname: this.lastnameValue,
                email: this.emailValue,
                cell: this.cellValue,
                dob: this.dobValue,
                stockId: this.data.Id,
                adults: this.adultsValue,
                under12: this.under12Value,
                note: this.note
              };
              this.service.enquire(enquiryData).subscribe(a => {
                console.log("The server response: " + a);
                this.openSnackBar("Enquiry successful", "Check you Emails");
                this.dialogRef.close();
              });

            } else {
              this.openSnackBar("What is you Date of Birth?", "required");
            }
          } else {
            this.openSnackBar("What is you Cell?", "required");
          }
        } else {
          this.openSnackBar("What is you email?", "required");
        }
      } else {
        this.openSnackBar("What is you lastname?", "required");
      }
    } else {
      this.openSnackBar("What is you name?", "required");
    }
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
