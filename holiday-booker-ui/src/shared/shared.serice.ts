import { HttpClient, HttpParams, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  Supplier, Resort, Region, Country, Vacation, ResortUnits,
  VacationSuppliers, UnitSizes, ResortWithRegionDto, VacationForDisplayDto,
  PublicFilterDto, PublicModelDto, Area, RegionDto, AreaDto, FilterStock, VacationList, CheckDuplicateDto, ListOfVacationDto, displayDuplicatesDto
} from './shared.models';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


@Injectable()
export class SharedService {
  constructor(private httpClient: HttpClient) {
  }

  connectionstring = environment.apiUrl;

  // Generating a Excel Spreadsheet
  generateExcelReport(T: FilterStock) {
    this.httpClient.post(this.connectionstring + 'api/exportToExcelwithuserid' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
      , observe: 'response', responseType: 'blob'
    }).pipe(map
      ((res: any) => {
        console.log('start download:', res);
        const url = window.URL.createObjectURL(res.body);
        const a = document.createElement('a');
        document.body.appendChild(a);
        a.setAttribute('style', 'display: none');
        a.href = url;
        a.download = 'HolidayBookerStock';
        a.click();
        window.URL.revokeObjectURL(url);
        a.remove(); // remove the element
      })).subscribe((res) => {
        console.log('File is downloaded');
      });
  }
  // Generating a Excel Spreadsheet
  generateExcelReport64(T: FilterStock) {
    this.httpClient.post(this.connectionstring + 'api/exportToExcel64withuserid' + "/" + localStorage.getItem('userId'), T).pipe(map
      ((res: any) => {
        console.log(res);
        //const url = window.URL.createObjectURL(res.body);
        const url = window.URL.createObjectURL(this.base64ToBlob(res.body));
        const a = document.createElement('a');
        document.body.appendChild(a);
        a.setAttribute('style', 'display: none');
        a.href = url;
        a.download = 'HolidayBookerStock';
        a.click();
        window.URL.revokeObjectURL(url);
        a.remove(); // remove the element
      })).subscribe((res) => {
        console.log('File is downloaded');
      });
  }

  public generateExcelReport64Test(T: FilterStock): any {
    let temp;
    if(T.theDate == null) {
      temp = 'null'
    } else {
      temp = T.theDate.toDateString();
    }
    return this.httpClient.get(this.connectionstring + 'api/exportToExcel64Getwithuserid/'+ T.supplierId + '/' +
    T.resortId + '/' + temp + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public base64ToBlob(b64Data, sliceSize = 512) {
    let byteCharacters = atob(b64Data); //data.file there
    let byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      let slice = byteCharacters.slice(offset, offset + sliceSize);

      let byteNumbers = new Array(slice.length);
      for (var i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }
      let byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }
    return new Blob(byteArrays, { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
  }

  // Supplier
  public getSuppliers(): Observable<Array<Supplier>> {
    return this.httpClient.get(this.connectionstring + 'api/supplierwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getSupplier(id: any): Observable<Supplier> {
    return this.httpClient.get(this.connectionstring + 'api/supplier/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteSupplier(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/supplier/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteSuppliersStock(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/vacation/deletebyproviderid/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addSupplier(T: Supplier): any {
    return this.httpClient.post(this.connectionstring + 'api/supplierwithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editSupplier(T: Supplier): any {
    return this.httpClient.put(this.connectionstring + 'api/supplier/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Resort
  public getResorts(): Observable<Array<Resort>> {
    return this.httpClient.get(this.connectionstring + 'api/resortwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getLatestResort(): Observable<Resort> {
    return this.httpClient.get(this.connectionstring + 'api/resortwithuserid/last' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getResortsWithRegion(): Observable<Array<ResortWithRegionDto>> {
    return this.httpClient.get(this.connectionstring + 'api/resortwithregionwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getResort(id: any): Observable<Resort> {
    return this.httpClient.get(this.connectionstring + 'api/resort/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteResort(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/resort/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addResort(T: Resort): any {
    return this.httpClient.post(this.connectionstring + 'api/resortwithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editResort(T: Resort): any {
    return this.httpClient.put(this.connectionstring + 'api/resort/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Region
  public getRegions(): Observable<Array<Region>> {
    return this.httpClient.get(this.connectionstring + 'api/regionwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getRegionsDto(): Observable<Array<RegionDto>> {
    return this.httpClient.get(this.connectionstring + 'api/regiondtowithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getRegion(id: any): Observable<Region> {
    return this.httpClient.get(this.connectionstring + 'api/region/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteRegion(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/region/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addRegion(T: Region): any {
    return this.httpClient.post(this.connectionstring + 'api/regionwithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editRegion(T: Region): any {
    return this.httpClient.put(this.connectionstring + 'api/region/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Area
  public getAreas(): Observable<Array<Area>> {
    return this.httpClient.get(this.connectionstring + 'api/areawithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getAreasDto(): Observable<Array<AreaDto>> {
    return this.httpClient.get(this.connectionstring + 'api/areadtowithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getArea(id: any): Observable<Area> {
    return this.httpClient.get(this.connectionstring + 'api/area/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteArea(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/area/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addArea(T: Area): any {
    return this.httpClient.post(this.connectionstring + 'api/areawithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editArea(T: Area): any {
    return this.httpClient.put(this.connectionstring + 'api/area/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Country
  public getCountries(): Observable<Array<Country>> {
    return this.httpClient.get(this.connectionstring + 'api/countrywithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getCountry(id: any): Observable<Country> {
    return this.httpClient.get(this.connectionstring + 'api/country/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteCountry(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/country/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addCountry(T: Country): any {
    return this.httpClient.post(this.connectionstring + 'api/countrywithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editCountry(T: Country): any {
    return this.httpClient.put(this.connectionstring + 'api/country/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // UnitSizes
  public getUnitSizes(): Observable<Array<UnitSizes>> {
    return this.httpClient.get(this.connectionstring + 'api/unitsizewithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getUnitSize(id: any): Observable<UnitSizes> {
    return this.httpClient.get(this.connectionstring + 'api/unitsize/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteUnitSize(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/unitsize/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addUnitSize(T: UnitSizes): any {
    return this.httpClient.post(this.connectionstring + 'api/unitsizewithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editUnitSize(T: UnitSizes): any {
    return this.httpClient.put(this.connectionstring + 'api/unitsize/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Vacation
  public getPartnersStock(): Observable<Array<VacationForDisplayDto>> {
    return this.httpClient.get(this.connectionstring + 'api/getPartnersStockwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getUserDefaultProfit(): Observable<number> {
    return this.httpClient.get(this.connectionstring + 'api/getuserdefaultprofitwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public updateDefaultPartnerPrice(price: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/updatedefaultpartnerpricewithuserid/' + price + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public updatePartnerStockProfit(stockId: number, price: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/updatepartnerstockprofit/' + stockId + "/" + price, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getVacations(): Observable<Array<Vacation>> {
    return this.httpClient.get(this.connectionstring + 'api/vacationwithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getVacationsForDisplay(): Observable<Array<VacationForDisplayDto>> {
    return this.httpClient.get(this.connectionstring + 'api/vacationfordisplaywithuserid' + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getVacation(id: any): Observable<Vacation> {
    return this.httpClient.get(this.connectionstring + 'api/vacation/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteVacation(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/vacation/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addVacation(T: Vacation): any {
    console.log(T);
    return this.httpClient.post(this.connectionstring + 'api/vacationwithuserid/add' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addVacationList(T: VacationList): any {
    console.log(T);
    return this.httpClient.post(this.connectionstring + 'api/vacationwithuserid/addlist' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getFilteredVaction(T: FilterStock): Observable<Array<VacationForDisplayDto>> {
    console.log(T);
    return this.httpClient.post(this.connectionstring + 'api/filtervacationwithuserid' + "/" + localStorage.getItem('userId'), T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public checkForDuplicates(T: ListOfVacationDto): Observable<Array<Vacation>> {
    return this.httpClient.post(this.connectionstring + 'api/checkforduplicates', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getFilteredVactionBySate(filter: any): Observable<Array<VacationForDisplayDto>> {
    return this.httpClient.get(this.connectionstring + 'api/filterbystatewithuserid/' + `${filter}` + "/" + localStorage.getItem('userId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getDuplicatesForDisplay(T: Vacation[]): Observable<Array<displayDuplicatesDto>> {
    return this.httpClient.post(this.connectionstring + 'api/getduplicatesfordisplay', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editVacation(T: Vacation): any {
    return this.httpClient.put(this.connectionstring + 'api/vacation/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editVacationSold(T: Vacation): any {
    return this.httpClient.put(this.connectionstring + 'api/vacation/edit/sold', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public editVacationHold(T: Vacation): any {
    return this.httpClient.put(this.connectionstring + 'api/vacation/edit/hold', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // ResortUnits
  public getResortUnits(): Observable<Array<ResortUnits>> {
    return this.httpClient.get(this.connectionstring + 'api/resortunits', {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getResortUnitsPerResort(id: any): Observable<Array<UnitSizes>> {
    return this.httpClient.get(this.connectionstring + 'api/unitsize/perresort/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getResortUnit(id: any): Observable<ResortUnits> {
    return this.httpClient.get(this.connectionstring + 'api/resortunits/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteResortUnits(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/resortunits/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteResortUnitsByResortId(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/resortunits/deletebyresort/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addResortUnits(T: ResortUnits): any {
    return this.httpClient.post(this.connectionstring + 'api/resortunits/add', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editResortUnits(T: ResortUnits): any {
    return this.httpClient.put(this.connectionstring + 'api/resortunits/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // VacationSuppliers
  public getVacationSuppliers(): Observable<Array<VacationSuppliers>> {
    return this.httpClient.get(this.connectionstring + 'api/vacationsuppliers', {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public getVacationSupplier(id: any): Observable<VacationSuppliers> {
    return this.httpClient.get(this.connectionstring + 'api/vacationsuppliers/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public deleteVacationSuppliers(id: any): any {
    return this.httpClient.delete(this.connectionstring + 'api/vacationsuppliers/delete/' + `${id}`, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public addVacationSuppliers(T: VacationSuppliers): any {
    return this.httpClient.post(this.connectionstring + 'api/vacationsuppliers/add', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
  public editVacationSuppliers(T: VacationSuppliers): any {
    return this.httpClient.put(this.connectionstring + 'api/vacationsuppliers/edit', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Public filtering
  public getFilteredStock(T: PublicFilterDto): Observable<Array<PublicModelDto>> {
    return this.httpClient.post(this.connectionstring + 'api/stock/filter', T, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Uploading a CSV File
  public uploadCSV(data: File, supplierName: string, adminFee: string): Observable<any> {
    console.log(data);
    const formData = new FormData();
    formData.append('file', data);
    const params = new HttpParams();
    const options = {
      params: params,
      reportProgress: false,
    };
    // 
    const req = new HttpRequest('POST', this.connectionstring + 'api/uploadwithuserid/vacation/csv/' + `${supplierName}` + '/' + `${adminFee}` + "/" + localStorage.getItem('userId'),
    formData );
    return this.httpClient.post(this.connectionstring + 'api/upload/vacation/csv/' + `${supplierName}` + '/' + `${adminFee}` + "/" + localStorage.getItem('userId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
}
