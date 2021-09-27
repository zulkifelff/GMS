import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {StartupRoutingModule} from './startup-routing.module';
import {IndexComponent} from './index/index.component';
import {MenuComponent} from './menu/menu.component';
import {AttendanceComponent} from './attendance/attendance.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {SharedModule} from "../shared/shared.module";
import { MembersComponent } from './members/members.component';
import { RolesComponent } from './roles/roles.component';
import { PackageComponent } from './package/package.component';
import { ReportsComponent } from './reports/reports.component';
import { HomeComponent } from './home/home.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatNativeDateModule, MatOptionModule} from '@angular/material/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {PaginationModule} from "ngx-bootstrap/pagination";
import {TooltipModule} from "ngx-bootstrap/tooltip";
import { AdditionComponent } from './membership/addition/addition.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatTooltipModule} from "@angular/material/tooltip";
import {MatSelectModule} from "@angular/material/select";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatListModule} from "@angular/material/list";
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  declarations: [IndexComponent, MenuComponent, AttendanceComponent, MembersComponent, RolesComponent, PackageComponent, ReportsComponent, HomeComponent, AdditionComponent],
  imports: [
    CommonModule,
    StartupRoutingModule,ModalModule.forRoot(),
    MatInputModule, MatNativeDateModule,
    MatSidenavModule, FormsModule, ReactiveFormsModule, SharedModule, MatFormFieldModule, MatDatepickerModule, PaginationModule, TooltipModule, MatSnackBarModule, MatButtonModule, MatIconModule, MatTooltipModule, MatSelectModule, MatOptionModule, MatToolbarModule, MatListModule
  ],
  exports: [IndexComponent, MenuComponent, AttendanceComponent, MembersComponent, RolesComponent, PackageComponent, ReportsComponent, HomeComponent, AdditionComponent]
})
export class StartupModule {
}
