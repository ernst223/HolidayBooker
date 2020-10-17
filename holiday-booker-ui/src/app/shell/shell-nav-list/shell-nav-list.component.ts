import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, Input, OnInit } from '@angular/core';
import * as _ from 'lodash';

import { MenuItem } from '../menu-item';
import { Router } from '@angular/router';

@Component({
  selector: 'app-shell-nav-list',
  templateUrl: './shell-nav-list.component.html',
  styleUrls: ['./shell-nav-list.component.scss'],
  animations: [
    trigger('parentActive', [
      state('inactive', style({
        transform: 'rotate(0deg)'
      })),
      state('active', style({
        transform: 'rotate(180deg)'
      })),
      transition('inactive => active', animate('150ms ease-in')),
      transition('active => inactive', animate('150ms ease-out'))
    ]),
    trigger('childrenActive', [
      state('inactive', style({
        opacity: '0',
        display: 'none'
      })),
      state('active', style({
        opacity: '1'
      })),
      transition('inactive => active', animate('150ms ease-in')),
      transition('active => inactive', animate('150ms ease-out'))
    ])
  ]
})
export class ShellNavListComponent implements OnInit {

  @Input() menuItems: MenuItem[];

  private activeParentMenuItems: string[] = [];

  constructor(
    private router: Router
  ) { }

  ngOnInit() {
    this.initializeMenuItems();
  }

  toggleActive(menuItem: MenuItem) {
    if (this.isParentActive(menuItem) === 'active') {
      _.remove(this.activeParentMenuItems, x => x === menuItem.title);
    }
    else {
      this.activeParentMenuItems.push(menuItem.title);
    }
  }

  isParentActive(menuItem: MenuItem) {
    const isActive = this.activeParentMenuItems.indexOf(menuItem.title) > -1;
    return isActive ? 'active' : 'inactive';
  }

  private initializeMenuItems() {
    for (let i = 0; i < this.menuItems.length; i++) {
      if (this.parentOrChildIsActive(this.menuItems[i])) {
        //this.activeParentMenuItems.push(this.menuItems[i].title);
      }
    }
  }

  private parentOrChildIsActive(menuItem: MenuItem) {
    if (this.router.isActive(menuItem.route, false)) {
      return true;
    }

    if (_.isNil(menuItem.children)) {
      return false;
    }

    for (let i = 0; i < menuItem.children.length; i++) {
      if (this.parentOrChildIsActive(menuItem.children[i])) {
        return true;
      }
    }

    return false;
  }
}

