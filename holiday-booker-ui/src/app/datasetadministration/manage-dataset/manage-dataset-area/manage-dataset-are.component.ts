import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort, MatPaginator, MatTableDataSource, MatSnackBar } from '@angular/material';
import { Area, Country, AreaDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-manage-dataset-are',
  templateUrl: './manage-dataset-are.component.html',
  styleUrls: ['./manage-dataset-are.component.scss']
})
export class ManageDatasetAreComponent implements OnInit {


  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  DescriptionValue: string;
  Data: AreaDto[];
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Id', 'Description', 'Country', 'Actions'];
  EditableId: number;
  SelectedCountry: string;
  countries: Country[];
  selectedRowIndex: number = -1;
  EditDescriptionValue: string;
  EditSelectedCountry: number;

  constructor(private _snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.GetCountries();
    this.setupDataStream();
  }

  ngAfterViewInit() {
    this.orderDataSource.sort = this.sort;
    this.orderDataSource.paginator = this.paginator;
  }
  GetCountries() {
    this.service.getCountries().subscribe(o => {
      this.countries = o;
    });
  }
  edit(id: any, editvalue: any, country: any) {
    this.EditableId = id;
    this.EditDescriptionValue = editvalue;
    this.selectedRowIndex = id;
    this.EditSelectedCountry = country;
  }
  ChangeEditMe() {
    this.EditableId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Area = {
      id: id,
      description: this.EditDescriptionValue,
      countryId: Number(this.EditSelectedCountry)
    };
    this.service.editArea(data).subscribe(a => {
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
    this.service.deleteArea(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBar('Deleted', '');
    });
  }

  add() {
    console.log(this.SelectedCountry);
    const data: Area = {
      id: 0,
      description: this.DescriptionValue,
      countryId: Number(this.SelectedCountry)
    };

    this.service.addArea(data).subscribe(a => {
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
    return this.service.getAreasDto();
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
