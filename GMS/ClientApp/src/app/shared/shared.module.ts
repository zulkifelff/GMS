import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { RightNavigationComponent } from './right-navigation/right-navigation.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";

import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material/core';
import {TooltipModule} from "ngx-bootstrap/tooltip";


@NgModule({
  declarations: [RightNavigationComponent],
    imports: [
        CommonModule, MatDatepickerModule, MatNativeDateModule,
        SharedRoutingModule, MatFormFieldModule,
        MatInputModule, TooltipModule
    ],
  exports: [RightNavigationComponent]
})
export class SharedModule { }
