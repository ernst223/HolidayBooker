export interface User {
  Id: number;
  Username: string;
  Password: string;
}
export interface Region {
  id: number;
  description: string;
  areaId: number;
}
export interface RegionDto {
  id: number;
  description: string;
  areaId: number;
  area: string;
}

export interface Area {
  id: number;
  description: string;
  countryId: number;
}
export interface AreaDto {
  id: number;
  description: string;
  countryId: number;
  country: string;
}
export interface Country {
  id: number;
  description: string;
}
export interface Resort {
  id: number;
  description: string;
  link: string;
  coordinates: string;
  regionId: number;
}

export interface ResortWithRegionDto {
  id: number;
  Description: string;
  Link: string;
  Region: string;
  RegionId: number;
  Area: string;
  Country: string;
  Coordinates: string;
}

export interface ResortUnits {
  Id: number;
  UnitSizeId: number;
  ResortId: number;
}
export interface Supplier {
  Id: number;
  Code: string;
  Description: string;
  Email: string;
  Contact: string;
}
export interface UnitSizes {
  id: number;
  description: string;
}
export interface Vacation {
  Id: number;
  ResortId: number;
  UnitSizeId: number;
  SupplierId: number;
  Arrival: Date;
  Nights: number;
  Price2Pay: number;
  BuyingPrice: number;
  AdminFee: number;
  Sold: boolean;
  Hold: boolean;
}

export class VacationList {
  myList: Vacation[];
}
export class CheckDuplicateDto {
  providerId: number;
  resortId: number;
  unitSizeId: number;
  arrival: Date;
  nights: number;
}
export class ListOfVacationDto {
  myList: CheckDuplicateDto[];
}
export class displayDuplicatesDto {
  id: number;
  resort: string;
  provider: string;
  arrival: string;
  nights: number;
}
export interface VacationForDisplayDto {
  Id: number;
  Resort: string;
  ResortId: number;
  Provider: string;
  ProviderId: number;
  UnitSize: string;
  UnitSizeId: number;
  Arrival: string;
  Nights: number;
  Price2Pay: number;
  BuyingPrice: number;
  AdminFee: number;
  Sold: boolean;
  Hold: boolean;
  PartnerPrice: number;
}

export interface FilterStock {
  supplierId: number;
  resortId: number;
  theDate: Date;
}

export interface PublicModelDto {
  Id: number;
  Resort: string;
  ResortId: number;
  Provider: string;
  ProviderId: number;
  UnitSize: string;
  UnitSizeId: number;
  Country: string;
  CountryId: number;
  Region: string;
  RegionId: number;
  Arrival: string;
  Nights: number;
  Price2Pay: number;
  BuyingPrice: number;
  AdminFee: number;
}

export interface PublicFilterDto {
resortId?: number;
arrivalIn: Date;
arrivalOut: Date;
maxAmount?: number;
minAmount?: number;
regionId?: number;
countryId?: number;
unitSizeId?: number;
}

export interface stockUnit {
  UnitId: string;
  Arrival: string;
  Nights: string;
  BuyingPrice: string;
  Price2Pay: string;
}

export interface VacationSuppliers {
  Id: number;
  VacationId: number;
  SupplierId: number;
}
export interface ResortDisplayDto {
  Id: number;
  Description: string;
  Region: string;
}
