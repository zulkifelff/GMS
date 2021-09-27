import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { RightNavigationComponent } from './right-navigation.component';

describe('RightNavigationComponent', () => {
  let component: RightNavigationComponent;
  let fixture: ComponentFixture<RightNavigationComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ RightNavigationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RightNavigationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
