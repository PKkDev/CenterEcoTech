import { HttpClientModule } from '@angular/common/http';
import { StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';

import { HeaderComponent } from './header.component';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HeaderComponent],
      imports: [HttpClientModule],
      providers: [AuthService, ApiService, baseAppUrl]
    })
      .compileComponents();

    fixture = TestBed.createComponent(HeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
