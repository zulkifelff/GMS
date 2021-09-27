import {Component, Input, OnInit} from '@angular/core';
import {Directive, HostListener} from '@angular/core';
import * as screenfull from 'screenfull';
import {ServiceCallService} from "../service-call.service";

@Component({
  selector: 'app-right-navigation',
  templateUrl: './right-navigation.component.html',
  styleUrls: ['./right-navigation.component.scss']
})
export class RightNavigationComponent implements OnInit {

  constructor(private SharedService:ServiceCallService) {
  }

  @Input() headingVar: string;

  ngOnInit(): void {
  }

  triggerFullScreen($event: MouseEvent) {
    console.log("Entering Fullscreen")
    if (screenfull.isEnabled) {
      if (screenfull.isEnabled) {
        screenfull.toggle().then(r => console.log(r));
      }
    }


  }

  toggleSideNavigationEvent($event: MouseEvent) {
    this.SharedService.sendToggleNavigationMessage($event);
  }
}
