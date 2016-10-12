import {Http,Headers, RequestOptions, HTTP_PROVIDERS} from '@angular/http';
import {Injectable} from '@angular/core';
import {Router}  from '@angular/router';

@Injectable()
export class LoginService{
    constructor(private _http:Http,private _router: Router){
       
    }

    
    public Login(username,password){
        let url = "http://localhost:64759/Token";
        let body = "username=" + username + "&password=" + password + "&grant_type=password";
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });   

        this._http.post(url, body, options).subscribe(
            response => {
                localStorage.setItem('access_token', response.json().access_token);
                localStorage.setItem('expires_in', response.json().expires_in);
                localStorage.setItem('token_type', response.json().token_type);
                localStorage.setItem('userName', response.json().userName);
                this._router.navigate(['pages/dashboard']);
            },
            error => {
                alert(error.text());
                console.log(error.text());
            }
        );     
    }
}