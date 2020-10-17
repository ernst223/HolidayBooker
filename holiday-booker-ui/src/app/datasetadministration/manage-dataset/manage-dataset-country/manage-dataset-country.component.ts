import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator } from '@angular/material';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SharedService } from 'src/shared/shared.serice';
import { Country } from 'src/shared/shared.models';

@Component({
  selector: 'app-manage-dataset-country',
  templateUrl: './manage-dataset-country.component.html',
  styleUrls: ['./manage-dataset-country.component.scss']
})
export class ManageDatasetCountryComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  DescriptionValue: string;
  Data: Country[];
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Id', 'Description', 'Actions'];
  EditableId: number;
  selectedRowIndex: number = -1;
  EditDescriptionValue: string;

  constructor(private _snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  ngAfterViewInit() {
    this.orderDataSource.sort = this.sort;
    this.orderDataSource.paginator = this.paginator;
  }

  edit(id: any, editvalue: any) {
    this.EditableId = id;
    this.EditDescriptionValue = editvalue;
    this.selectedRowIndex = id;
  }
  ChangeEditMe() {
    this.EditableId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Country = {
      id: id,
      description: this.EditDescriptionValue
    };
    this.service.editCountry(data).subscribe(a => {
      this.openSnackBar('Update Successful', '');
      this.EditDescriptionValue = '';
      this.ChangeEditMe();
      this.selectedRowIndex = -1;
    });
    }
   testEditabliity(id: any): boolean {
    if (id == this.EditableId) {
      return true;
    } else {
      return false;
    }
  }

  delete(id: any) {
    this.service.deleteCountry(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBar('Deleted', '');
    });
  }

  add() {
    const data: Country = {
      id: 0,
      description: this.DescriptionValue
    };

    this.service.addCountry(data).subscribe(a => {
      this.openSnackBar('Successful', '');
      this.DescriptionValue = '';
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.orderDataSource.filter = filterValue;
  }

  // Loading the data from server
  loadData() {
    return this.service.getCountries();
  }
  setupDataStream() {
    this.loadData().subscribe(a => {
      this.Data = a;
      this.orderDataSource.data = this.Data;
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.setupDataStream();
  }
}
