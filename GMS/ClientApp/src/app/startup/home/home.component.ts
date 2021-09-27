import { Component, OnInit } from '@angular/core';
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  passingHeading='Home';

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  triggerAddMember($event: MouseEvent) {
    debugger
    this.router.navigate(['startup','membership', 'addition']).then(r  =>console.log(r));
  }
}
