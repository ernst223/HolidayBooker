import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDatasetAreComponent } from './manage-dataset-are.component';

describe('ManageDatasetAreComponent', () => {
  let component: ManageDatasetAreComponent;
  let fixture: ComponentFixture<ManageDatasetAreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManageDatasetAreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageDatasetAreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
