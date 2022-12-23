import { HttpClientModule } from '@angular/common/http';
import { StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';

import { IndicationsPageComponent } from './indications-page.component';

describe('IndicationsPageComponent', () => {
  let component: IndicationsPageComponent;
  let fixture: ComponentFixture<IndicationsPageComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, ReactiveFormsModule],
      declarations: [IndicationsPageComponent],
      providers: [AuthService, ApiService, baseAppUrl]
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
