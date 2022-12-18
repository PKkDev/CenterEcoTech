import { HttpClientModule } from '@angular/common/http';
import { StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';

import { ApplicationsPageComponent } from './applications-page.component';

describe('ApplicationsPageComponent', () => {
  let component: ApplicationsPageComponent;
  let fixture: ComponentFixture<ApplicationsPageComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, ReactiveFormsModule],
      declarations: [ApplicationsPageComponent],
      providers: [AuthService, ApiService, baseAppUrl]
    })
      .compileComponents();

    fixture = TestBed.createComponent(ApplicationsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
