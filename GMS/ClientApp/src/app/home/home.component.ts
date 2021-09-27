import { AfterViewInit, Component, OnDestroy, Renderer2 } from '@angular/core';
import { OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy, AfterViewInit {

  public counterPlayed = 0;
  public globalInstance: void;
  constructor(private router: Router) {

  }
  ngOnInit() {
    document.querySelector('video').addEventListener('ended', () => {
      console.count('loop restart');
      this.counterPlayed = this.counterPlayed + 1;
      console.log(this.counterPlayed);
    });
  }

  ngOnDestroy() {

  }

  ngAfterViewInit(): void {
    
   
  }

  ContinueApplication($event: MouseEvent) {
    console.log($event);
    this.router.navigate(['/login'])
  }
}

