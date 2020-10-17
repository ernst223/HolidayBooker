import { Component, OnInit, Inject } from '@angular/core';
import { MatTableDataSource, MatSnackBar, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormControl, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-view-stock',
  templateUrl: './view-stock.component.html',
  styleUrls: ['./view-stock.component.scss']
})
export class ViewStockComponent implements OnInit {

  providerList: string[] = ['ER116','EP225','JC156','ER225'];
  providerControl = new FormControl();

  resorts: any[] = [
    {value: 'MountAmanzi', viewValue: 'MountAmanzi'},
    {value: 'Klein Kariba', viewValue: 'Klein Kariba'},
    {value: 'Buffelspoort', viewValue: 'Buffelspoort'}
  ];
  unitSize: any[] = [
    {value: '4 Bed', viewValue: '4 Bed'},
    {value: '6 Bed', viewValue: '6 Bed'},
    {value: '8 Bed', viewValue: '8 Bed'}
  ];

  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Provider', 'Resort', 'Unitsize','Arrival','Nights','BuyingPrice','Price2Pay','AdminFee','State', 'Actions'];
  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ViewStockComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  edit(){

  }
}
