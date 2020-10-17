import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UploadcsvComponent } from './uploadcsv.component';

describe('UploadcsvComponent', () => {
  let component: UploadcsvComponent;
  let fixture: ComponentFixture<UploadcsvComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UploadcsvComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UploadcsvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
