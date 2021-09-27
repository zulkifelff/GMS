import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {ServiceCallService} from "../../shared/service-call.service";
import {Subscription} from "rxjs";
import {MatSidenav} from "@angular/material/sidenav";

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.scss'],
  host: {
    class:'IndexComponentClass'
  }
})
export class IndexComponent implements OnInit {

  options: FormGroup;
  widthFalse: boolean;
  @ViewChild('sidenav') public leftSideNavigation: MatSidenav;
  public subscription: Subscription;

  constructor(fb: FormBuilder, private SharedService: ServiceCallService) {
    this.widthFalse = false;
    this.options = fb.group({
      bottom: 0,
      fixed: false,
      top: 0
    });
    this.subscription = this.SharedService.getToggleNavigationMessage().subscribe(message => {
      if (message) {
        this.leftSideNavigation.toggle().then(r => console.log('Toggling Side Navigation'));
      }
    });
  }

  ngOnInit() {
  }


}
