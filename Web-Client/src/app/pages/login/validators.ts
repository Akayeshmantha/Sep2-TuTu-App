import {FormControl} from '@angular/forms'; 
import {Http,Headers, RequestOptions, HTTP_PROVIDERS} from '@angular/http';
import {Injectable,Injector} from '@angular/core';

export class LoginValidators{
    constructor(private _http:Http){
        
    }
    static cannotContainSpace(control:FormControl){
        if(control.value.indexOf(' ') >= 0){
            return {cannotContainSpace : true};
        }
        return null;
    }

    static passwordStrength(control:FormControl){
        if(control.value.length >= 4 && control.value.length <= 6){
            return {passwordStrength : "weak"};
        }else if(control.value.length >= 6 && control.value.length <= 8){
            return {passwordStrength : "good"};
        }
        return null;
    }

}