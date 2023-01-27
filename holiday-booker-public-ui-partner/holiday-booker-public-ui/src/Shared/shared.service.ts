import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PublicModelDto, PublicFilterDto, Country, Region, Area, FastFilterDto, EnquiryDto } from './shared.models';

@Injectable()
export class SharedService {
  constructor(private httpClient: HttpClient) {
  }
  connectionstring = environment.apiUrl;

  public getAllStock(): Observable<Array<PublicModelDto>> {
    return this.httpClient.get(this.connectionstring + 'api/stock/allpartner').pipe(map((res: any) => res));
  }
  public clean(): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/cleanup').pipe(map((res: any) => res));
  }
  public getFilteredStock(T: PublicFilterDto): Observable<Array<PublicModelDto>> {
    return this.httpClient.post(this.connectionstring + 'api/stock/filterpartner', T).pipe(map((res: any) => res));
  }
  public getFastFilteredStock(T: FastFilterDto): Observable<Array<PublicModelDto>> {
    return this.httpClient.post(this.connectionstring + 'api/stock/fastfilterpartner', T).pipe(map((res: any) => res));
  }
  public getCountries(): Observable<Array<Country>> {
    return this.httpClient.get(this.connectionstring + 'api/country').pipe(map((res: any) => res));
}
public getRegions(): Observable<Array<Region>> {
  return this.httpClient.get(this.connectionstring + 'api/region').pipe(map((res: any) => res));
}
public getAreas(countryId: any): Observable<Array<Area>> {
  return this.httpClient.get(this.connectionstring + 'api/areapercountry/' + countryId).pipe(map((res: any) => res));
}

//Calling the Email service
public enquire(T: EnquiryDto): Observable<any> {
  return this.httpClient.post(this.connectionstring + 'api/enquirypartner', T).pipe(map((res: any) => res));
}
public addNewEmail(email: any): Observable<any> {
  return this.httpClient.get(this.connectionstring + 'api/newClientPartner/' + email).pipe(map((res: any) => res));
}
}
