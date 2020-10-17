import 'rxjs/add/operator/map';
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable()
export class UserLoggedInGuard implements CanActivate {
    constructor(
        private router: Router
    ) { }

    canActivate() {
        const isLoggedIn = JSON.parse(localStorage.getItem('account'));
        if (isLoggedIn.loggedIn) {
            return true;
        } else {
            this.router.navigate(['/account/login']);
            return false;
        }
    }
}
