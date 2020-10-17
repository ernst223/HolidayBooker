import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewduplicatesComponent } from './viewduplicates.component';

describe('ViewduplicatesComponent', () => {
  let component: ViewduplicatesComponent;
  let fixture: ComponentFixture<ViewduplicatesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewduplicatesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewduplicatesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
