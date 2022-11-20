import { Component, OnInit,Directive,Input, ViewChild } from '@angular/core';
import { MenuService } from './menu.service';
import { MenuItem } from './menu-item';
import { Observable } from 'rxjs';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { map } from 'rxjs/operators';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { MatSidenav } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.scss']
})

export class ShellComponent implements OnInit {

  
  menuItems: MenuItem[] = [];
  @ViewChild('sidenav', {static: false}) sidenav: MatSidenav;

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
  .pipe(
    map(result => result.matches)
  );
  constructor(private menuService: MenuService,  private breakpointObserver: BreakpointObserver, private router: Router) { }

  ngOnInit() {
    this.menuItems = this.menuService.getMenuItems();
  }

  signOut() {
    this.router.navigate(['']);
    localStorage.clear();
  }

  onClick() {
      console.log(`isOpen: ${this.sidenav.opened}`);
  }
}
