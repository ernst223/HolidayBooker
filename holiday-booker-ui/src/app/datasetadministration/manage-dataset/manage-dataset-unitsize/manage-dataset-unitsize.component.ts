import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSnackBar, MatPaginator, MatSort } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { UnitSizes } from 'src/shared/shared.models';

@Component({
  selector: 'app-manage-dataset-unitsize',
  templateUrl: './manage-dataset-unitsize.component.html',
  styleUrls: ['./manage-dataset-unitsize.component.scss']
})
export class ManageDatasetUnitsizeComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  DescriptionValue: string;
  Data: UnitSizes[];
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  EditableId: number;
  selectedRowIndex: number = -1;
  EditDescriptionValue: string;
  displayedColumns = ['Id', 'Description', 'Actions'];
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
    const data: UnitSizes = {
      id: id,
      description: this.EditDescriptionValue
    };
    this.service.editUnitSize(data).subscribe(a => {
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
    this.service.deleteUnitSize(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBar('Deleted', '');
    });
  }
  add() {
    const data: UnitSizes = {
      id: 0,
      description: this.DescriptionValue
    };

    this.service.addUnitSize(data).subscribe(a => {
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
    return this.service.getUnitSizes();
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
