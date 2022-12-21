import { HttpClientModule } from '@angular/common/http';
import { ChangeDetectorRef, StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from '../../authorize/services/auth.service';

import { RegistrationComponent } from './registration.component';

describe('RegistrationComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegistrationComponent],
      imports: [HttpClientModule, FormsModule],
      providers: [ChangeDetectorRef, AuthService, ApiService, baseAppUrl]
    })
      .compileComponents();

    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
