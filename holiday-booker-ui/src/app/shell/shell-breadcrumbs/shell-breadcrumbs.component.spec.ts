import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShellBreadcrumbsComponent } from './shell-breadcrumbs.component';

describe('ShellBreadcrumbsComponent', () => {
  let component: ShellBreadcrumbsComponent;
  let fixture: ComponentFixture<ShellBreadcrumbsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShellBreadcrumbsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShellBreadcrumbsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
