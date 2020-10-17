export interface PublicFilterDto {
id: number;
arrivalIn: Date;
arrivalOut: Date;
maxAmount: number;
minAmount: number;
regionId: number;
areaId: number;
countryId: number;
unitSizeId: number;
}

export interface PublicModelDto {
  id: number;
  resort: string;
  resortId: number;
  link: string;
  country: string;
  countryId: number;
  area: string;
  areaId: number;
  region: string;
  regionId: number;
  unitSizeId: number;
  unitSize: string;
  arrival: string;
  nights: number;
  price2Pay: number;
  sold: boolean;
  hold: boolean;
}
export interface FastFilterDto {
  CountryId: number;
  AreaId: number[];
  Arrival: Date;
  Nights: number;
}

export interface Country {
  id: number;
  description: string;
}

export interface EnquiryDto {
  name: string;
  lastname: string;
  dob: string;
  cell: string;
  email: string;
  stockId: number;
  adults: number;
  under12: number;
  note: string;
}

export interface Region {
  id: number;
  description: string;
  areaId: number;
}

export interface Area {
  id: number;
  description: string;
  countryId: number;
}
