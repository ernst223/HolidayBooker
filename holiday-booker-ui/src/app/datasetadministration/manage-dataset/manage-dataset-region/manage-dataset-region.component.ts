import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatSnackBar } from '@angular/material';
import { Region, Country, Area, RegionDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-manage-dataset-region',
  templateUrl: './manage-dataset-region.component.html',
  styleUrls: ['./manage-dataset-region.component.scss']
})
export class ManageDatasetRegionComponent implements OnInit {

  @ViewChild(MatSort,{static: false}) sort: MatSort;
  @ViewChild(MatPaginator,{static: false}) paginator: MatPaginator;
  DescriptionValue: string;
  EditDescriptionValue: string;
  SelectedArea: string;
  areas: Area[];
  Data: RegionDto[];
  EditableId: number;
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Id', 'Description', 'Area', 'Actions'];
  selectedRowIndex: number = -1;
  EditSelectedArea: number;

  constructor(private _snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.GetAreas();
    this.setupDataStream();
  }
  ngAfterViewInit(){
    this.orderDataSource.sort = this.sort;
    this.orderDataSource.paginator = this.paginator;
  }
  GetAreas() {
    this.service.getAreas().subscribe(o => {
      this.areas = o;
    });
  }
  edit(id: any, editvalue: any, area: any){
    this.EditableId = id;
    this.EditDescriptionValue = editvalue;
    this.selectedRowIndex = id;
    this.EditSelectedArea = area;
  }
  delete(id: any){
    this.service.deleteRegion(id).subscribe(a =>{
      console.log("Entity deleted");
      this.openSnackBar("Deleted", "");
    });
  }
  add(){
    var data: Region = {
      id: 0,
      description: this.DescriptionValue,
      areaId: Number(this.SelectedArea)
    }

    this.service.addRegion(data).subscribe(a => {
      this.openSnackBar("Successful", "");
      this.DescriptionValue = "";
    });
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.orderDataSource.filter = filterValue;
  }

  //Loading the data from server
  loadData(){
    return this.service.getRegionsDto();
  }
   setupDataStream(){
     this.loadData().subscribe(a => {
       this.Data = a;
       this.orderDataSource.data = this.Data;
     });
   }

   ChangeEditMe() {
    this.EditableId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Region = {
      id: id,
      description: this.EditDescriptionValue,
      areaId: Number(this.EditSelectedArea)
    };
    this.service.editRegion(data).subscribe(a => {
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
   openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.setupDataStream();
  }

}
