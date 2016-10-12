import {Component, ViewEncapsulation} from '@angular/core';
import {FormGroup, AbstractControl, FormBuilder, Validators} from '@angular/forms';
import {LoginValidators} from './validators';
import {Location} from '@angular/common';
import {EmailValidator} from '../../theme/validators/email.validator';
import {HTTP_PROVIDERS } from '@angular/http';
import {LoginService} from './login.service';
    

@Component({
  selector: 'login',
  encapsulation: ViewEncapsulation.None,
  directives: [],
  providers: [LoginService,HTTP_PROVIDERS],
  styles: [require('./login.scss')],
  template: require('./login.html'),
})
export class Login {

  public form:FormGroup;
  public email:AbstractControl;
  public password:AbstractControl;
  public submitted:boolean = false;

  constructor(fb:FormBuilder,private _location: Location,private _loginService: LoginService) {
    this.form = fb.group({
      'email': ['', Validators.compose([
        Validators.required,
        Validators.minLength(4)])],
      'password': ['', Validators.compose([
        Validators.required,
        Validators.minLength(4)])]
    });

    this.email = this.form.controls['email'];
    this.password = this.form.controls['password'];
  }

  public onSubmit(values:Object):void {
    this.submitted = true;
    if (this.form.valid) {
      this._loginService.Login(values["email"], values['password']);
    }

  }
}
