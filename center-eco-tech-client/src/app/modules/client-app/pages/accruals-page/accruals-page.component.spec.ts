import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccrualsPageComponent } from './accruals-page.component';

describe('AccrualsPageComponent', () => {
  let component: AccrualsPageComponent;
  let fixture: ComponentFixture<AccrualsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccrualsPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccrualsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
