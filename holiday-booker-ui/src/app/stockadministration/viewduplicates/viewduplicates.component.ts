import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ListOfVacationDto, Vacation, displayDuplicatesDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-viewduplicates',
  templateUrl: './viewduplicates.component.html',
  styleUrls: ['./viewduplicates.component.scss']
})
export class ViewduplicatesComponent implements OnInit {

  myData: displayDuplicatesDto[];
  constructor(public dialogRef: MatDialogRef<ViewduplicatesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Vacation[], private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service.getDuplicatesForDisplay(this.data).subscribe(a => {
      this.myData = a;
    });
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
}
