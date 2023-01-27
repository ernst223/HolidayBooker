import { Component, OnInit, Inject, ViewChild, ElementRef } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSnackBar, MatDialog } from '@angular/material';
import { PublicModelDto, Country, Region, Area, FastFilterDto } from 'src/Shared/shared.models';
import { SharedService } from 'src/Shared/shared.service';
import { EnquiryComponent } from '../enquiry/enquiry.component';
import { Router } from '@angular/router';
import { PageScrollService } from 'ngx-page-scroll-core';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-public-ui',
  templateUrl: './public-ui.component.html',
  styleUrls: ['./public-ui.component.scss']
})
export class PublicUiComponent implements OnInit {

  displayedColumns = ['Resort', 'Unitsize', 'Region', 'Arrival', 'Nights', 'Price2Pay',
    'Actions'];
  nights = ['3', '4', '5', '7', '8', '10'];
  nightFilter = '7';
  selectedCountry: number;
  Data: PublicModelDto[];
  Countries: Country[];
  Areas: Area[];
  AreaChoices: boolean[] = [];
  selectAll: boolean;
  ArrivalIn: Date;
  DataSource: MatTableDataSource<object> = new MatTableDataSource();
  areaFilter: number[] = [];
  SelectedDate: any;
  chosenNight: any;
  SubmitEmailValue: string;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild('AboutSection', { static: false }) public aboutStructure: ElementRef;
  @ViewChild('ContactSection', { static: false }) public contactStructure: ElementRef;

  constructor(private pageScrollService: PageScrollService, @Inject(DOCUMENT) private document: any
    , private router: Router, private dialog: MatDialog, private service: SharedService, private _snackBar: MatSnackBar) { }

  ngOnInit() {
    this.chosenNight = 7;
    this.getCountries();
  }

  onCountryChange() {
    this.getRegions();
  }

  submitEmail() {
    this.service.addNewEmail(this.SubmitEmailValue).subscribe(a => {
      this.openSnackBar('Successful', 'Close');
    });
  }
  testToEnquire(id: any): boolean {
    let data = this.Data.find(o => o.id == id);
    if (data.sold == false) {
      if (data.hold == false) {
        return true;
      }
    }
    return false
  }
  ngAfterViewInit() {
    this.DataSource.paginator = this.paginator;
  }
  selectThemAll() {
    if (this.selectAll == true) {
      this.AreaChoices = [];
      this.Areas.forEach(element => {
        this.AreaChoices.push(true);
      });
    } else {
      this.AreaChoices = [];
      this.Areas.forEach(element => {
        this.AreaChoices.push(false);
      });
    }
  }
  public moveToAbout(): void {
    this.router.navigate(['/about']);
    //this.aboutStructure.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }
  public moveToContact(): void {
    this.contactStructure.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end', inline: 'start' });
  }

  navigateToAbout() {
    console.log("Scrolling to About");
    let el = document.getElementById('AboutSection');
    el.scrollTop = el.scrollHeight;
  }

  setupDataStream() {
    this.service.getAllStock().subscribe(a => {
      this.Data = a;
      this.DataSource.data = this.Data;
    });
  }
  getCountries() {
    this.service.getCountries().subscribe(a => {
      this.Countries = a;
      this.selectedCountry = this.Countries.find(o => o.description == 'South Africa').id;
      this.getRegions();
    });
  }
  getRegions() {
    this.service.getAreas(this.selectedCountry).subscribe(a => {
      this.Areas = a;
      this.Areas.forEach(element => {
        this.AreaChoices.push(false);
      });
      this.setupDataStream();
    });
  }
  info(id: any) {
    console.log("Enquire for a Holiday: " + id);
    const dialogRef = this.dialog.open(EnquiryComponent, {
      data: {
        Id: id
      }
    });
    // tslint:disable-next-line: align
    dialogRef.afterClosed().subscribe(() => {
      // Going to about page
      //this.router.navigate(['/about']);
      window.location.reload();
    });
  }

  Filter() {
    let count = 0;
    this.AreaChoices.forEach(element => {
      if (element == true) {
        this.areaFilter.push(this.Areas[count].id);
      }
      count++;
    });
    let tempDate = this.SelectedDate;
    if (tempDate == undefined) {
      tempDate = null;
    }
    const data: FastFilterDto = {
      CountryId: Number(this.selectedCountry),
      AreaId: this.areaFilter,
      Arrival: tempDate,
      Nights: Number(this.chosenNight)
    };
    this.service.getFastFilteredStock(data).subscribe(a => {
      console.log("Data filtered, Scroll Down");
      this.Data = a;
      this.DataSource.data = this.Data;
      this.openSnackBar("Scroll Down", "Close");
      this.clearData();
    });
    // console.log(this.SelectedDate);
    // console.log(this.chosenNight);
  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      duration: 4000,
    });
  }
  clearData() {
    this.areaFilter = [];
  }
  addDays(date: Date, days: number): Date {
    console.log('adding ' + days + ' days');
    console.log(date);
    date.setDate(date.getDate() + Number(days));
    console.log(date);
    return date;
  }
}

