import {Http,Headers, RequestOptions, HTTP_PROVIDERS} from '@angular/http';
import {Injectable} from '@angular/core';
import {Router}  from '@angular/router';
var alertify = require('alertify.js');

@Injectable()
export class RegisterService{
    

    constructor(private _http:Http,private _router: Router){

    }

    register(name,password,confirmPassword,email,company){
        let url = "http://localhost:64759/api/Account/Register";

        let body = {"UserName" : name ,
                    "Password" : password,  
                    "ConfirmPassword":confirmPassword,
                    "Email": email,
                    "Company": company};
        
        var data = JSON.stringify(body);

        let headers = new Headers(
            { 'Content-Type': 'application/json' });
        
        let options = new RequestOptions({ headers: headers });   
        
        this._http.post(url,data, options).subscribe(
            response => {
               
                if(response.ok == true && response.statusText == "OK"){
                    alertify.success("Welcome To Tu_Tu");
                    let url = "http://localhost:64759/Token";
                    let body = "username=" + name + "&password=" + password + "&grant_type=password";
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
                            alertify.error("Error In Registration Please Check Credentials");
                            console.log(error.text());
                        }
                    );     
                }else{
                    alertify.error("Error In Registration Please Check Credentials");
                }
            },
            error => {
                alertify.error("Error In Registration Please Check Credentials");
            }
        );

    }
    
}