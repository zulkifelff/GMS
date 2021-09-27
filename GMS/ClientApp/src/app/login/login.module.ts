import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginPageComponent } from './login-page/login-page.component';
import {ReactiveFormsModule} from "@angular/forms";


@NgModule({
  declarations: [LoginPageComponent],
    imports: [
        CommonModule,
        LoginRoutingModule,
        ReactiveFormsModule
    ],
  exports: [LoginPageComponent]
})
export class LoginModule { }
