import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndicationsPageComponent } from './indications-page.component';

describe('IndicationsPageComponent', () => {
  let component: IndicationsPageComponent;
  let fixture: ComponentFixture<IndicationsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndicationsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndicationsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
