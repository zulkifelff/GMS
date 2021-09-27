import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {Router} from "@angular/router";
import {ServiceCallService} from "../../shared/service-call.service";
import {takeUntil} from "rxjs/operators";
import {Subject, throwError} from "rxjs";
import {FormValueDTO, LoginHttpResponse} from "../../shared/shared-dto";


@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  LoginFormBuilder: FormGroup;
  Submitted = false;
  Submit_Message: string;
  private ngUnsubscribe: Subject<any> = new Subject();

  constructor(private FormBuilder: FormBuilder,private router: Router,private _SharedServiceCall:ServiceCallService) { }

  ngOnInit() {

    this.LoginFormBuilder = this.FormBuilder.group({
      EmployeeNumber: ["", Validators.required],
      Password: ["", [Validators.required, Validators.minLength(4)]]
    });
  }

  get f() { return this.LoginFormBuilder.controls; }

  onSubmit() {

    this.Submitted = true;

    // stop here if form is invalid
    if (this.LoginFormBuilder.invalid) {
      return;
    }

    // display form values on success
    const FormValue:FormValueDTO=this.LoginFormBuilder.getRawValue();

    this._SharedServiceCall.GetLoginToken(FormValue)
      .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      (incomingResponse: LoginHttpResponse) => {
        if(incomingResponse)
        {
          let AuthToken=incomingResponse.Token;
          let UserId=incomingResponse.Id;
          let Username=incomingResponse.EmployeeNumber;
          let Password=incomingResponse.Password;

          this._SharedServiceCall.SetupLocalStorage('AuthToken',AuthToken);
          this._SharedServiceCall.SetupLocalStorage('UserId',UserId.toString());
          this._SharedServiceCall.SetupLocalStorage('Username',Username);
          this._SharedServiceCall.SetupLocalStorage('Password',Password);
        }

      },
      (err) => {

        console.log('Error while getting Lease List', err);
        return throwError(
          'Something bad happened; please try again later.');
      }
    );




  }
}
