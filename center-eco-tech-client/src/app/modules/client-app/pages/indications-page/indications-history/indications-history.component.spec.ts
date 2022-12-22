import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndicationsHistoryComponent } from './indications-history.component';

describe('IndicationsHistoryComponent', () => {
  let component: IndicationsHistoryComponent;
  let fixture: ComponentFixture<IndicationsHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndicationsHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndicationsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
