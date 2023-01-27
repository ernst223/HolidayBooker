import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { ActivatedRoute } from '@angular/router';

@Injectable()
export class MenuService {

  constructor( private router: ActivatedRoute) { }

  getMenuItems(): MenuItem[] {

    const items: MenuItem[] = [];

      items.push({
        title: 'Providers',
        route: '/administration/manage-providers',
        icon: 'assignment_ind'
      });
      items.push({
        title: 'Resorts',
        route: '/resortadministration/manage-resort',
        icon: 'store'
      });
      items.push({
        title: 'Stock',
        route: '/stockadministration/manage-stock',
        icon: 'monetization_on',
      });
      items.push({
        title: 'Partners Stock',
        route: '/stockadministration/view-partner-stock',
        icon: 'monetization_on',
      });
      if (localStorage.getItem('userName') == 'info@holidaybooker.co.za') {
        items.push({
          title: 'Dataset',
          route: '/datasetadministration/manage-dataset',
          icon: 'line_style',
        });
      }

    return items;
  }
}
