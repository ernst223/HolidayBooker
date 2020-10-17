import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable()
export class AccountService {

    constructor(private httpClient: HttpClient) { }

    connectionstring = environment.apiUrl;

    public login(username: string, password: string): Observable<Boolean> {
        const body = '{ UserName: "' + username + '",Password: "' + password + '"}';
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            // tslint:disable-next-line: align
            return this.httpClient.post(this.connectionstring + 'api/auth/token', body, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                if (typeof res.token !== 'undefined') {
                    // Stores access token & refresh token.
                    localStorage.setItem('access_token', res.token);
                    return true;
                } else {
                    return false;
                }
            }));
    }

    public logout() {
        return this.httpClient.get('auth/logout');
    }
}
