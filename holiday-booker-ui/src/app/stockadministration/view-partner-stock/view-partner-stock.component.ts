import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { VacationForDisplayDto } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
    selector: 'app-view-partner-stock',
    templateUrl: './view-partner-stock.component.html',
    styleUrls: ['./view-partner-stock.component.scss']
})
export class ViewPartnerStockComponent implements OnInit {

    @ViewChild(MatSort, { static: true }) sort: MatSort;
    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;

    selectedRowIndex: number = -1;
    EditableId: number;
    EditMyPriceValue: number;
    DefaultProfit = 0;
    Data: VacationForDisplayDto[];

    orderDataSource: MatTableDataSource<object> = new MatTableDataSource();

    displayedColumns = ['Provider', 'Resort', 'Unitsize', 'Arrival', 'Nights', 'BuyingPrice', 'MyPrice', 'Profit', 'Actions'];

    constructor(public snackBar: MatSnackBar, private service: SharedService) { }

    ngOnInit() {
        this.orderDataSource.sort = this.sort;
        this.setupdatastream();
    }

    edit(id: any, partnerPrice: any) {
        this.EditableId = id;
        this.EditMyPriceValue = partnerPrice;
        this.selectedRowIndex = id;
    }

    SaveEdit(id: any) {
        // update stock my price
        this.service.updatePartnerStockProfit(this.EditableId, this.EditMyPriceValue).subscribe(a => {
            this.ChangeEditMe();
            this.setupdatastream();
            this.openSnackBar("Changes saved", "close");
        });
    }

    openSnackBar(message: string, action: string) {
        this.snackBar.open(message, action, {
          duration: 2000,
        });
      }

    ChangeEditMe() {
        this.EditableId = null;
        this.selectedRowIndex = -1;
    }

    ngAfterViewInit() {
        this.orderDataSource.sort = this.sort;
        this.orderDataSource.paginator = this.paginator;
    }

    getProfit(buyingprice: any, price2pay: any): number {
        return Number(price2pay) - Number(buyingprice);
    }

    testEditabliity(id: any): boolean {
        if (id == this.EditableId) {
            return true;
        } else {
            return false;
        }
    }

    getUserProvider() {
        if (localStorage.getItem('userId') == '75c81754-5a66-45f9-8b05-060bf16434b9') {
            return 'EZT Travel'
        } else {
            return 'HolidayBooker'
        }
    }

    setupdatastream() {
        this.service.getUserDefaultProfit().subscribe(a => {
            this.DefaultProfit = a;
        });
        this.service.getPartnersStock().subscribe(a => {
            this.Data = a;
            this.orderDataSource.data = this.Data;
        })
    }

    applyFilter(filterValue: string) {
        filterValue = filterValue.trim();
        filterValue = filterValue.toLowerCase();
        this.orderDataSource.filter = filterValue;
    }

    saveDefaultProfit() {
        this.service.updateDefaultPartnerPrice(this.DefaultProfit).subscribe(a => {
            this.openSnackBar("Changes saved", "close");
        });
    }
}
