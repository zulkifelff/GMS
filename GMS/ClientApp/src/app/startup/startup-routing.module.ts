import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {IndexComponent} from "./index/index.component";
import {AttendanceComponent} from "./attendance/attendance.component";
import {MembersComponent} from "./members/members.component";
import {RolesComponent} from "./roles/roles.component";
import {PackageComponent} from "./package/package.component";
import {ReportsComponent} from "./reports/reports.component";
import {HomeComponent} from "./home/home.component";
import {AdditionComponent} from "./membership/addition/addition.component";


const routes: Routes = [{
  path: '', component: IndexComponent, children: [
    {path: 'home', component: HomeComponent},
    {path: 'attendance', component: AttendanceComponent},
    {path: 'members', component: MembersComponent},
    {path: 'roles', component: RolesComponent},
    {path: 'package', component: PackageComponent},
    {path: 'reports', component: ReportsComponent},
    {
      path:'membership',children:[
        {path: 'addition', component: AdditionComponent},
      ]
    }
  ]
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StartupRoutingModule {
}
