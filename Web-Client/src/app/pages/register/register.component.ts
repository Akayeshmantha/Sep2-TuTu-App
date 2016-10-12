import {Component, ViewEncapsulation} from '@angular/core';
import {FormGroup, AbstractControl, FormBuilder, Validators} from '@angular/forms';
import {EmailValidator, EqualPasswordsValidator} from '../../theme/validators';
import {Http,Headers, RequestOptions, HTTP_PROVIDERS} from '@angular/http';
import {RegisterService} from './resgister.service';
import {LoginValidators} from '../login/validators';
import {FormControl} from '@angular/forms';
import {LoginService} from '../login/login.service';

@Component({
  selector: 'register',
  encapsulation: ViewEncapsulation.None,
  directives: [],
  providers: [RegisterService,HTTP_PROVIDERS,LoginService],
  styles: [require('./register.scss')],
  template: require('./register.html'),
})
export class Register {

  public form:FormGroup;

  public name:AbstractControl;
  
  public email:AbstractControl;
  
  public password:AbstractControl;
  
  public repeatPassword:AbstractControl;
  
  public passwords:FormGroup;
  
  public company:AbstractControl;

  public submitted:boolean = false;
    
  public isLoading = false;    
  
  constructor(fb:FormBuilder,private _registerService:RegisterService,private _http:Http,private _loginService:LoginService) {

    this.form = fb.group({
      'name': ['', Validators.compose([Validators.required, Validators.minLength(4)]),
      this.checkUserNameAvailbility.bind(this)],

      'email': ['', Validators.compose([Validators.required, 
      EmailValidator.validate,LoginValidators.cannotContainSpace,
      ]),this.checkEmailAvailability.bind(this)],

      'company': ['', Validators.compose([Validators.required, 
      ,LoginValidators.cannotContainSpace
      ])],
      
      'passwords': fb.group({
        'password': ['', Validators.compose([Validators.required, Validators.minLength(4),
        LoginValidators.cannotContainSpace,
        LoginValidators.passwordStrength])],
        'repeatPassword': ['', Validators.compose([Validators.required, Validators.minLength(4)])]
      }, {validator: EqualPasswordsValidator.validate('password', 'repeatPassword')})
    });

    this.name = this.form.controls['name'];
    this.email = this.form.controls['email'];
    this.passwords = <FormGroup> this.form.controls['passwords'];
    this.password = this.passwords.controls['password'];
    this.repeatPassword = this.passwords.controls['repeatPassword'];
    this.company = this.form.controls['company'];
  }

  public onSubmit(values:Object):void {
    this.submitted = true;
    this.isLoading = true;
    
    if (this.form.valid) {
       var Email =values['email'];
       
       var Name  = values['name']
       
       var Password = values['passwords'];
       
       var Company = values['company'];
       
       this._registerService.register(Name,Password['password'],Password['repeatPassword'],Email,Company);
    }
  }

  
     checkUserNameAvailbility(control:FormControl){
        return new Promise((resolve,reject) => {
            this._http.get('http://localhost:64759/api/Account/CheckUserName?UserName='+control.value+'').subscribe(
                response => {
                    if(!response){
                    resolve({ShouldBeUnique: true});
                    }
                    resolve(null);
                }
            );
        });
    }

    checkEmailAvailability(control:FormControl){
       return new Promise((resolve,reject) => {
            this._http.get('http://localhost:64759/api/Account/CheckEmail?email='+control.value+'').subscribe(
                response => {
                    if(!response){
                    resolve({ShouldBeUnique: true});
                    }
                    resolve(null);
                }
            );
        });
    }

}
