import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ShellNavListComponent } from './shell-nav-list.component';

describe('ShellNavListComponent', () => {
  let component: ShellNavListComponent;
  let fixture: ComponentFixture<ShellNavListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ShellNavListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ShellNavListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
