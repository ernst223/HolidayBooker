import { Component, OnInit, Inject } from '@angular/core';
import { MatTableDataSource, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder } from '@angular/forms';
import { inject } from '@angular/core/testing';

@Component({
  selector: 'app-view-resort',
  templateUrl: './view-resort.component.html',
  styleUrls: ['./view-resort.component.scss']
})
export class ViewResortComponent implements OnInit {

  regions: any[] = [
    {value: 'NW', viewValue: 'NW'},
    {value: 'Eastern Cape', viewValue: 'Eastern Cape'},
    {value: 'Northen Cape', viewValue: 'Northern Cape'}
  ];
  countries: any[] = [
    {value: 'South Africa', viewValue: 'South Africa'},
    {value: 'Mozambique', viewValue: 'Mozambique'},
    {value: 'Namibia', viewValue: 'Namibia'}
  ];
  unitSize: any[] = [
    {value: '4 Bed', viewValue: '4 Bed'},
    {value: '6 Bed', viewValue: '6 Bed'},
    {value: '8 Bed', viewValue: '8 Bed'}
  ];
  
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Description', 'Region', 'Units', 'Actions'];
  UnitCount: number[] = [1];
  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder,
     public dialogRef: MatDialogRef<ViewResortComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  edit(){

  }

  addUnit(){
    this.UnitCount.push(this.UnitCount.length);
  }
}
