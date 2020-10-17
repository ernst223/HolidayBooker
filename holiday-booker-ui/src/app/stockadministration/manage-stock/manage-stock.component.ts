import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatTableDataSource, MatDialog, MatPaginator, MatSort, MatSnackBar } from '@angular/material';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Resort, UnitSizes, Supplier, Vacation, stockUnit, VacationForDisplayDto, FilterStock, VacationList, ListOfVacationDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';
import { startWith, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ViewduplicatesComponent } from '../viewduplicates/viewduplicates.component';
import { UploadcsvComponent } from '../uploadcsv/uploadcsv/uploadcsv.component';



@Component({
  selector: 'app-manage-stock',
  templateUrl: './manage-stock.component.html',
  styleUrls: ['./manage-stock.component.scss']
})
export class ManageStockComponent implements OnInit {
  [x: string]: any;

  DataToAdd: Vacation;
  DataToAddObject: VacationList = new VacationList();
  DataToAddArray: Vacation[] = [];
  listOfVacForDuplicate: ListOfVacationDto = { myList: [] };

  myDuplicate: Vacation;
  Duplicates: Vacation[] = [];
  providerControl = new FormControl();
  autoNights: number[] = [3, 4, 5, 7, 8, 10];

  // 2 way binding in a ngfor
  stockUnits: stockUnit[] = [
    {
      UnitId: '', Arrival: '', Nights: '', BuyingPrice: '', Price2Pay: ''
    }
  ];
  // 2 way binding in a ngfor

  NewVacationSold: Vacation;
  NewVacationHold: Vacation;
  SoldValue: boolean;
  HoldValue: boolean;
  providerList: Supplier[];
  resorts: Resort[];
  myControl: FormControl = new FormControl();
  filteredOptions: Observable<Resort[]>;

  unitSize: UnitSizes[];
  unitSizeFilter: UnitSizes[];

  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

  SelectedProvider: string;
  SelectedProviderFilter: string;
  SelectedResort: string;
  SelectedResortObject: Resort;
  SelectedResortFilter: string;
  SelectedResortObjectFilter: Resort;
  adminFee = 99;
  ArrivalFiltered: Date;

  Data: VacationForDisplayDto[];
  UnitCount: number[] = [1];
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Provider', 'Resort', 'Unitsize', 'Arrival', 'Nights', 'BuyingPrice', 'Price2Pay', 'Profit', 'AdminFee'
    , 'Hold', 'State', 'Actions'];
  HoldSoldFilter = [{ value: 0, viewValue: 'No filter' },
  { value: 1, viewValue: 'Hold' },
  { value: 2, viewValue: 'Sold' }];
  SelectedStateFilter: any;

  constructor(private _snackBar: MatSnackBar, private service: SharedService, public dialog: MatDialog) {

  }

  ngOnInit() {
    this.orderDataSource.sort = this.sort;
    this.setupDataStream();
  }
  filterAccordingToState() {
    console.log(this.SelectedStateFilter);
    this.service.getFilteredVactionBySate(this.SelectedStateFilter).subscribe(a => {
      this.Data = a;
      this.orderDataSource.data = this.Data;
    });
  }
  private _filter(value: string): Resort[] {
    if (value != null) {
      if ((typeof value) === 'string') {
        const filterValue = value.toLowerCase();
        return this.resorts.filter(option => option.description.toLowerCase().includes(filterValue));
      }
    } else {
      return this.resorts;
    }
  }

  EditableId: number;
  EditArivalValue: Date;
  EditNightsValue: string;
  EditBuyingPriceValue: string;
  EditPrice2PayValue: string;
  EditAdminFeeValue: string;
  EditProviderValue: string;
  EditResortValue: string;
  EditUnitSizeValue: string;
  selectedRowIndex: number = -1;
  edit(id: any, providerid: any, resortid: any, unitsizeid: any, arrival: any, nights: any, buyingprice: any, price2pay: any, adminfee) {
    this.EditableId = id;
    this.EditProviderValue = providerid;
    this.EditResortValue = resortid;
    this.EditUnitSizeValue = unitsizeid;
    this.EditArivalValue = new Date(arrival);
    this.EditNightsValue = nights;
    this.EditBuyingPriceValue = buyingprice;
    this.EditPrice2PayValue = price2pay;
    this.EditAdminFeeValue = adminfee;
    this.selectedRowIndex = id;
    console.log(this.EditArivalValue);
    console.log(arrival);
  }
  ChangeEditMe() {
    this.EditableId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Vacation = {
      Id: id,
      Arrival: this.EditArivalValue,
      Nights: Number(this.EditNightsValue),
      BuyingPrice: Number(this.EditBuyingPriceValue),
      Price2Pay: Number(this.EditPrice2PayValue),
      AdminFee: Number(this.EditAdminFeeValue),
      SupplierId: Number(this.EditProviderValue),
      ResortId: Number(this.EditResortValue),
      UnitSizeId: Number(this.EditUnitSizeValue),
      Sold: false,
      Hold: false
    };
    this.service.editVacation(data).subscribe(a => {
      this.openSnackBarForEdit('Update Successful', '');
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

  displayData(data?: Resort): string | undefined {
    return data ? data.description : undefined;
  }
  getProfit(buyingprice: any, price2pay: any): number {
    return Number(price2pay) - Number(buyingprice);
  }
  resortSelected(option: any) {
    console.log(option.option.value);
    this.SelectedResort = option.option.value.id;
    this.SelectedResortObject = option.option.value;
  }

  resortSelectedFilter(option: any) {
    console.log(option.option.value);
    this.SelectedResortFilter = option.option.value.id;
    this.SelectedResortObjectFilter = option.option.value;
  }

  ngAfterViewInit() {
    this.orderDataSource.sort = this.sort;
    this.orderDataSource.paginator = this.paginator;
  }

  // Loading the data from server
  loadData() {
    return this.service.getVacationsForDisplay();
  }

  advanceFilter() {
    console.log('Doing a advance filter');
    if (this.SelectedProviderFilter === undefined) {
      this.SelectedProviderFilter = '0';
    }
    if (this.SelectedResortFilter === undefined) {
      this.SelectedResortFilter = '0';
    }
    if (this.ArrivalFiltered === undefined) {
      this.ArrivalFiltered = null;
    }

    const data: FilterStock = {
      supplierId: Number(this.SelectedProviderFilter),
      resortId: Number(this.SelectedResortFilter),
      theDate: this.ArrivalFiltered
    };

    this.service.getFilteredVaction(data).subscribe(a => {
      this.Data = a;
      this.orderDataSource.data = this.Data;
    });
  }
  downloadExcel() {
    console.log('Downloading Excel File');
    this.openSnackBarForUpload('Pleas wait for Download', 'close');
    if (this.SelectedProviderFilter === undefined) {
      this.SelectedProviderFilter = '0';
    }
    if (this.SelectedResortFilter === undefined) {
      this.SelectedResortFilter = '0';
    }
    if (this.ArrivalFiltered === undefined) {
      this.ArrivalFiltered = null;
    }

    const data: FilterStock = {
      supplierId: Number(this.SelectedProviderFilter),
      resortId: Number(this.SelectedResortFilter),
      theDate: this.ArrivalFiltered
    };

    //this.service.generateExcelReport64(data);
    this.service.generateExcelReport64Test(data).subscribe(res => {
      console.log("Value returned....");
      console.log(res);

      const url = window.URL.createObjectURL(this.service.base64ToBlob(res.body));
      const a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = url;
      a.download = 'HolidayBookerStock';
      a.click();
      window.URL.revokeObjectURL(url);
      a.remove();
    });
  }

  setupDataStream() {
    this.GetResorts();
    this.GetSuppliers();
    this.GetUnitSizes();
    this.loadData().subscribe(a => {
      this.Data = a;
      this.orderDataSource.data = this.Data;
    });
  }

  clearFilter() {
    this.SelectedProvider = undefined;
    this.ArrivalFiltered = undefined;
    this.SelectedResortFilter = undefined;
    this.SelectedStateFilter = -1;
    this.loadData().subscribe(a => {
      this.Data = a;
      this.orderDataSource.data = this.Data;
    });
  }
  GetResorts() {
    this.service.getResorts().subscribe(p => {
      this.resorts = p;
      this.filteredOptions = this.myControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this._filter(value))
        );
    });
  }
  GetSuppliers() {
    this.service.getSuppliers().subscribe(o => {
      this.providerList = o;
    });
  }
  GetUnitSizes() {
    this.service.getUnitSizes().subscribe(o => {
      this.unitSize = o;
    });
  }
  UnitSizeSelected() {
    this.service.getResortUnitsPerResort(Number(this.myControl.value.id)).subscribe(a => {
      this.unitSizeFilter = a;
    });
  }

  openSnackBarForUpload(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.setupDataStream();
  }
  delete(id: any) {
    this.service.deleteVacation(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBarForDelete('Deleted', '');
    });
  }
  openSnackBarForDelete(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.advanceFilter();
  }
  openSnackBarForEdit(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 2000,
    });
    this.advanceFilter();
  }

  removeUnit(i: number) {
    this.stockUnits.splice(i, 1);
  }
  add() {
    // Firstly testing if all values are entered
    if (this.SelectedResortObject != undefined) {
      if (this.SelectedProvider != undefined) {

        //Secondly Testing if there is a positive Profit
        var proceed = true;
        this.stockUnits.forEach(element => {
          if (Number(element.BuyingPrice) > Number(element.Price2Pay)) {
            proceed = false;
          }
        });

        if (proceed == true) {
          let counter = 0;
          this.stockUnits.forEach(element => {
            if (element.UnitId !== '' || element.UnitId == null) {
              counter++;
              this.DataToAdd = {
                Id: 0,
                ResortId: this.SelectedResortObject.id,
                SupplierId: Number(this.SelectedProvider),
                UnitSizeId: Number(element.UnitId),
                Arrival: new Date(element.Arrival),
                Nights: Number(element.Nights),
                Price2Pay: Number(element.Price2Pay),
                BuyingPrice: Number(element.BuyingPrice),
                AdminFee: this.adminFee,
                Sold: false,
                Hold: false
              };

              this.DataToAddArray.push(this.DataToAdd);
              // this.service.addVacation(this.DataToAdd).subscribe(a => {
              //   if (a == true) {
              //     if (counter == this.stockUnits.length) {
              //       this.openSnackBar('Successful', '');
              //     }
              //   } else {
              //     this.openSnackBarForUpload('Oops! Failed', '');
              //   }
              // });
            }
          });

          this.DataToAddObject.myList = this.DataToAddArray;
          console.log(this.DataToAddObject);
          this.service.addVacationList(this.DataToAddObject).subscribe(a => {
            if (a == true) {
              this.openSnackBarForUpload('Successful', '');
              this.setupDataStream();
            } else {
              this.openSnackBarForUpload('Failed to upload', '');
            }
            this.DataToAddArray = [];
          });
        } else {
          this.openSnackBar("Buying Price larger than Price2Pay", "Profit Error");
        }
      } else {
        this.openSnackBar("Please select a Provider", "Close");
      }
    } else {
      this.openSnackBar("Please select a Resort", "Close");
    }
  }

  checkForDuplicates() {
    if (this.SelectedResortObject != undefined) {
      if (this.SelectedProvider != undefined) {
        //Secondly Testing if there is a positive Profit
        var proceed = true;
        this.stockUnits.forEach(element => {
          if (Number(element.BuyingPrice) > Number(element.Price2Pay)) {
            proceed = false;
          }
        });
        if (proceed == true) {

          this.stockUnits.forEach(element => {
            if (element.UnitId !== '' || element.UnitId == null) {
              this.DataToAdd = {
                Id: 0,
                ResortId: this.SelectedResortObject.id,
                SupplierId: Number(this.SelectedProvider),
                UnitSizeId: Number(element.UnitId),
                Arrival: new Date(element.Arrival),
                Nights: Number(element.Nights),
                Price2Pay: Number(element.Price2Pay),
                BuyingPrice: Number(element.BuyingPrice),
                AdminFee: this.adminFee,
                Sold: false,
                Hold: false
              };

              this.DataToAddArray.push(this.DataToAdd);
            }
          });

          // Know starting to check for duplicates
          let testDuplicate = false;

          this.DataToAddArray.forEach(element => {
            this.listOfVacForDuplicate.myList.push({
              providerId: element.SupplierId,
              resortId: element.ResortId,
              unitSizeId: element.UnitSizeId,
              arrival: element.Arrival,
              nights: element.Nights
            });
          });

          this.service.checkForDuplicates(this.listOfVacForDuplicate).subscribe(a => {
            console.log(a);
            if (a.length == 0) {
              this.openSnackBarForUpload('No Duplicates Found', 'Close');
              this.add();
            } else {
              this.openSnackBarForUpload('Found Duplicates', 'Close')
              //Open Dialog with result a
              this.openDialog(a);
            }

          })
        } else {
          this.openSnackBar("Buying Price larger than Price2Pay", "Profit Error");
        }
      } else {
        this.openSnackBar("Please select a Provider", "Close");
      }
    } else {
      this.openSnackBar("Please select a Resort", "Close");
    }

    //Clear Data Here
    this.DataToAddArray = [];
    this.listOfVacForDuplicate.myList = [];
  }

  openDialog(T: Vacation[]): void {
    const dialogRef = this.dialog.open(ViewduplicatesComponent, {
      width: '800px',
      data: T
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }

  delay(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }


  clearValues() {
    this.SelectedProvider = null;
    this.SelectedResort = null;
    this.SelectedResortObject = null;
    this.stockUnits = [{ UnitId: '', Arrival: '', Nights: '', BuyingPrice: '', Price2Pay: '' }];
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.orderDataSource.filter = filterValue;
  }
  addUnit() {
    this.stockUnits.push({ UnitId: '', Arrival: '', Nights: '', BuyingPrice: '', Price2Pay: '' });
  }
  trackByIndex(index: number, obj: any): any {
    return index;
  }

  // Sold Checkbox pressed
  Sold(value: any, id: any) {
    if (value === 'true' || value === true) {
      this.SoldValue = false;
    } else {
      this.SoldValue = true;
    }
    this.service.getVacation(id).subscribe(a => {
      this.NewVacationSold = a;
      this.NewVacationSold.Sold = this.SoldValue;
      this.updateSold();
    });
  }

  updateSold() {
    // Updating the Entity
    this.service.editVacationSold(this.NewVacationSold).subscribe(a => {
      console.log('Updated the new entity');
      this.openSnackBarForUpload('Updated', '');
    });
  }

  // Hold Checkbox pressed
  Hold(value: any, id: any) {
    if (value === 'true' || value === true) {
      this.HoldValue = false;
    } else {
      this.HoldValue = true;
    }
    this.service.getVacation(id).subscribe(a => {
      this.NewVacationHold = a;
      this.NewVacationHold.Hold = this.HoldValue;
      this.updateHold();
    });
  }

  updateHold() {
    // Updating the Entity
    this.service.editVacationHold(this.NewVacationHold).subscribe(a => {
      console.log('Update the new entity');
      this.openSnackBarForUpload('Updated', '');
    });
  }

  getSoldStatus(status: any): boolean {
    if (status === 'true' || status === true) {
      return true;
    } else {
      return false;
    }
  }

  getHoldStatus(status: any): boolean {
    if (status === 'true' || status === true) {
      return true;
    } else {
      return false;
    }
  }

  uploadCSV(){
    const dialogRef = this.dialog.open(UploadcsvComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      this.animal = result;
    });
  }
}
