
import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Params, Router } from '@angular/router';
import * as _ from 'lodash';
import { Subscription } from 'rxjs';
import { filter, takeUntil, distinctUntilChanged } from 'rxjs/operators';

// import { Subscription } from 'rxjs/Subscription';

interface Breadcrumb {
  label: string;
  params: Params;
  url: string;
}

@Component({
  selector: 'app-shell-breadcrumbs',
  templateUrl: './shell-breadcrumbs.component.html',
  styleUrls: ['./shell-breadcrumbs.component.scss']
})
export class ShellBreadcrumbsComponent implements OnInit, OnDestroy {
  breadcrumbs: Breadcrumb[];
  private navigationEndSubscription: Subscription;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) {
    this.navigationEndSubscription = this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      distinctUntilChanged()
    ).subscribe(event => {
      this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
    });
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.navigationEndSubscription.unsubscribe();
  }

  private buildBreadCrumb(route: ActivatedRoute, url: string = '', breadcrumbs: Breadcrumb[] = []): Breadcrumb[] {
    const ROUTE_DATA_BREADCRUMB = 'breadcrumb';

    if (!_.isNil(route.routeConfig)) {
      url += `${url.endsWith('/') ? '' : '/'}${route.routeConfig.path}`;
    }

    if (_.isNil(_.get(route, `routeConfig.data.${ROUTE_DATA_BREADCRUMB}`, null))) {
      return this.buildBreadCrumb(route.firstChild, url, breadcrumbs);
    }

    // add breadcrumb
    const breadcrumb: Breadcrumb = {
      label: route.snapshot.data[ROUTE_DATA_BREADCRUMB],
      params: route.snapshot.params,
      url: _.trimEnd(url, '/')
    };

    if (breadcrumbs.length === 0) {
      breadcrumbs.push(breadcrumb);
    }
    else if (_.last(breadcrumbs).label === breadcrumb.label) {
      breadcrumbs.splice(breadcrumbs.length - 1, 1, ...[breadcrumb]);
    }
    else {
      breadcrumbs.push(breadcrumb);
    }

    if (_.isNil(route.firstChild)) {
      return breadcrumbs;
    }

    return this.buildBreadCrumb(route.firstChild, url, breadcrumbs);
  }
}
