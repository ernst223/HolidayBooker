import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatDialog, MatPaginator, MatSort, MatSnackBar } from '@angular/material';
import { ViewResortComponent } from '../view-resort/view-resort.component';
import { Resort, Region, Country, UnitSizes, ResortUnits, ResortWithRegionDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';
import { FormControl } from '@angular/forms';
import { startWith, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-manage-resorts',
  templateUrl: './manage-resorts.component.html',
  styleUrls: ['./manage-resorts.component.scss']
})
export class ManageResortsComponent implements OnInit {

  // This is testing values--------------------------------------------------------
  // regions: any[] = [
  //   {value: 'NW', viewValue: 'NW'},
  //   {value: 'Eastern Cape', viewValue: 'Eastern Cape'},
  //   {value: 'Northen Cape', viewValue: 'Northern Cape'}
  // ];
  // countries: any[] = [
  //   {value: 'South Africa', viewValue: 'South Africa'},
  //   {value: 'Mozambique', viewValue: 'Mozambique'},
  //   {value: 'Namibia', viewValue: 'Namibia'}
  // ];
  // unitSize: any[] = [
  //   {value: '4 Bed', viewValue: '4 Bed'},
  //   {value: '6 Bed', viewValue: '6 Bed'},
  //   {value: '8 Bed', viewValue: '8 Bed'}
  // ];
  // Testing values--------------------------------------------------------

  regions: Region[];
  countries: Country[];
  unitSize: UnitSizes[];
  unitIds: number[] = [-1];
  tempLatestData: Resort;
  tempUnitResort: ResortUnits;
  showUnitSize: number;
  editUnitSizeControl = new FormControl();
  UnitSizeControl = new FormControl();
  myControl: FormControl = new FormControl();
  filteredOptions: Observable<Region[]>;

  @ViewChild(MatSort, { static: false }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  DescriptionValue: string;
  LinkValue: string;
  CoordinateValue = '';
  SelectedCountry: string;
  SelectedRegion: string;
  SelectedRegionObject: Region;

  EditDescriptionValue: string;
  EditLinkValue: string;
  EditSelectedCountry: number;
  EditSelectedRegion: number;
  EditCoordinates: string;

  EditMeId: number;

  Data: ResortWithRegionDto[];
  resortDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Description', 'Region', 'Area', 'Country', 'Coordinates', 'units', 'Link' , 'Actions'];
  constructor(private dialog: MatDialog, private _snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.GetRegions();
    this.setupDataStream();
  }
  ngAfterViewInit() {
    this.resortDataSource.sort = this.sort;
    this.resortDataSource.paginator = this.paginator;
  }

  //Resort Autocomplete
  setResort(){

  }

  // Loading the data from server
  loadData() {
    return this.service.getResortsWithRegion();
  }
  setupDataStream() {
    this.loadData().subscribe(a => {
      this.Data = a;
      this.resortDataSource.data = this.Data;
      this.service.getUnitSizes().subscribe(b => {
        this.unitSize = b;
      });
    });
  }
  trackByIndex(index: number, obj: any): any {
    return index;
  }

  GetRegions() {
    this.service.getRegions().subscribe(p => {
      this.regions = p;
      this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
    });
  }
  private _filter(value: string): Region[] {
    if (value != null) {
      if ((typeof value) === 'string') {
        const filterValue = value.toLowerCase();
        return this.regions.filter(option => option.description.toLowerCase().includes(filterValue));
      }
    } else {
      return this.regions;
    }
  }
  displayData(data?: Region): string | undefined {
    return data ? data.description : undefined;
  }
  regionSelected(option: any) {
    this.SelectedRegion = option.option.value;
    this.SelectedRegionObject = option.option.value;
  }

  removeUnit(i: number) {
    this.unitIds.splice(i, 1);
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.setupDataStream();
  }

  edit(id: any, description: any, link: any, region: any, coordinates: any) {
    // const dialogRef = this.dialog.open(ViewResortComponent, {
    //      data: {
    //        id: id
    //      }
    //    });
    //    dialogRef.afterClosed().subscribe( a => {
    //      this.setupDataStream();
    //    });
    //this.openSnackBar('Button not yet working', '');
    this.selectedRowIndex = id;
    this.closeShowUnitSizes();
    this.EditDescriptionValue = description;
    this.EditLinkValue = link;
    this.EditSelectedRegion = region;
    this.EditMeId = id;
    this.EditCoordinates = coordinates;
  }
  testEditMe(id: any): boolean {
    if (id == this.EditMeId) {
      return true;
    } else {
      return false;
    }
  }
  ChangeEditMe() {
    this.EditMeId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Resort = {
      id: id,
      description: this.EditDescriptionValue,
      link: this.EditLinkValue,
      coordinates: this.EditCoordinates,
      regionId: Number(this.EditSelectedRegion)
    };
    this.service.editResort(data).subscribe(a => {
      this.openSnackBar('Update Successful', '');
      this.EditDescriptionValue = '';
      this.EditLinkValue = '';
      this.EditCoordinates = '';
      this.EditSelectedCountry = null;
      this.EditSelectedRegion = null;
      this.ChangeEditMe();
      this.selectedRowIndex = -1;
    });
  }

  delete(id: any) {
    this.service.deleteResort(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBar('Deleted', '');
    });
  }
  add() {
    const data: Resort = {
      id: 0,
      description: this.DescriptionValue,
      link: this.LinkValue,
      coordinates: this.CoordinateValue,
      // regionId: Number(this.SelectedRegion)
      regionId: this.SelectedRegionObject.id
    };

    this.service.addResort(data).subscribe(a => {
      this.uploadUnits();
      this.openSnackBar('Successful', '');
      this.clearForm();
    });
  }

  unitIdsNew: UnitSizes[];
  uploadUnits() {
    this.service.getLatestResort().subscribe(a => {
      this.tempLatestData = a;
      this.UnitSizeControl.value.forEach(entity => {
        this.tempUnitResort = {
          Id: 0,
          ResortId: this.tempLatestData.id,
          UnitSizeId: entity.id
        };
        console.log(this.tempUnitResort);
        this.service.addResortUnits(this.tempUnitResort).subscribe(res => {
          console.log("Unit Resort Uploaded to server, Unit Resort Id:" + entity);
        });
      });
      // this.unitIds.forEach(entity => {
      //   if(entity>0){
      //     this.tempUnitResort = {
      //       Id: 0,
      //       ResortId: this.tempLatestData.id,
      //       UnitSizeId: entity
      //      };
      //      this.service.addResortUnits(this.tempUnitResort).subscribe(res =>{
      //        console.log("Unit Resort Uploaded to server, Unit Resort Id:"+entity );
      //      });
      //   }else{
      //     console.log("Unit Size where not selected. Id equals:"+entity);
      //   }
      // });
    });
  }
  clearForm() {
    this.DescriptionValue = '';
    this.LinkValue = '';
    this.SelectedCountry = null;
    this.SelectedRegion = null;
    this.unitIds = [-1];
    this.tempLatestData = null;
    this.tempUnitResort = null;
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.resortDataSource.filter = filterValue;
  }
  addUnit() {
    this.unitIds.push(-1);
  }


  //Displaying the entities current UnitSizes in the multi select***********
  selectedUnitSizeEditObject: UnitSizes[];
  showUnitSizes(id: any) {
    this.service.getResortUnitsPerResort(id).subscribe(o => {
      this.ChangeEditMe();
      this.showUnitSize = id;
      this.selectedUnitSizeEditObject = o;
      let idList = [];
      for (const a of o) {
        idList.push(a.id);
      }
      this.editUnitSizeControl.setValue(idList);
      this.selectedRowIndex = id;
    });
  }
  testShowUnitSize(id: any): boolean {
    if (id == this.showUnitSize) {
      return true;
    } else {
      return false;
    }
  }
  closeShowUnitSizes() {
    this.showUnitSize = null;
  }
  saveUnitSizes(id: any) {
    this.service.deleteResortUnitsByResortId(id).subscribe(o => {
      console.log("Old Data are deleted");
      this.storeNewUnitSizes(id);
    });
    this.closeShowUnitSizes();
    this.selectedRowIndex = -1;
  }


  compareUnits(c1: UnitSizes, c2: UnitSizes) {
    return c1.description === c2.description;
    //return c1 && c2 ? c1.Id === c2.Id : c1 === c2;
  }

  storeNewUnitSizes(id: any) {
    this.editUnitSizeControl.value.forEach(element => {
      this.tempUnitResort = {
        Id: 0,
        ResortId: id,
        UnitSizeId: element
      };
      this.service.addResortUnits(this.tempUnitResort).subscribe(res => {
        console.log("Unit Resort Uploaded to server, Unit Resort Id:" + element);
        this.openSnackBar("UnitSizes Updated", "");
      });
    });
  }

  selectedRowIndex: number = -1;

  selectRow(row: any) {
    // if(this.selectedRowIndex === row.id){
    //   this.unselectRow();
    // }
    // else{
    //   this.selectedRowIndex = row.id;
    // }
  }
  unselectRow() {
    this.selectedRowIndex = -1;
  }

}
