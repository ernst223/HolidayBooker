import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-administration-component',
  template: `
      <router-outlet></router-outlet>
      `
})
export class AdministrationComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
