import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationsHistoryComponent } from './applications-history.component';

describe('ApplicationsHistoryComponent', () => {
  let component: ApplicationsHistoryComponent;
  let fixture: ComponentFixture<ApplicationsHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationsHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApplicationsHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
