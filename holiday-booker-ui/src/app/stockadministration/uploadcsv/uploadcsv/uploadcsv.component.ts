import { Component, Inject, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-uploadcsv',
  templateUrl: './uploadcsv.component.html',
  styleUrls: ['./uploadcsv.component.scss']
})
export class UploadcsvComponent implements OnInit {

  theFile: any;
  fileName = '';
  supplierName = '';
  adminFee = '99';
  canUpload = false;
  uploading = false;
  constructor(public dialogRef: MatDialogRef<UploadcsvComponent>, private _snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 3500,
    });
  }
  OpenFile(event: any) {
    // The file is in this event
    // console.log(event.target.files[0].name);
    this.theFile = event.target.files[0];
    this.fileName = event.target.files[0].name;
    this.canUpload = true;
    this.openSnackBar('File Ready For Processing', 'Close');
  }
  upload() {
    if (this.canUpload == true) {
      this.uploading = true;
      this.openSnackBar('This may take a while', 'Close');
      this.service.uploadCSV(this.theFile, this.supplierName, this.adminFee).subscribe(a => {
        console.log(a);
        this.openSnackBar('Process Completed', 'Close');
        this.expFile(a);
        this.dialogRef.close();
      });
    } else {
      this.openSnackBar('Choose a File', 'Close');
    }
  }

  saveTextAsFile(data) {

    if (!data) {
      console.error('Console.save: No data')
      data = 'NO ERRORS FOUND'
    }

    var blob = new Blob([data], { type: 'text/plain' })
    // FOR IE:
    let url = window.URL.createObjectURL(blob);
    let a = document.createElement('a');
    document.body.appendChild(a);
    a.setAttribute('style', 'display: none');
    a.href = url;
    a.download = "Error_Report.txt";
    a.click();
    window.URL.revokeObjectURL(url);
    a.remove();

  }

  expFile(results: string[]) {
    var fileText = '';
    results.forEach(a => {
      fileText += a + '\r\n'
    });
    this.saveTextAsFile(fileText);
  }
}
