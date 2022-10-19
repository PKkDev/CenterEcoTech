/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement, StaticProvider } from '@angular/core';

import { LogInPageComponent } from './log-in-page.component';
import { AuthService } from '../services/auth.service';
import { ApiService } from 'src/app/services/api.service';
import { HttpClientModule } from '@angular/common/http';

describe('LogInPageComponent', () => {

  let component: LogInPageComponent;
  let fixture: ComponentFixture<LogInPageComponent>;

  beforeEach(async () => {

    const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };
    // TODO
    await TestBed.configureTestingModule({
      declarations: [LogInPageComponent],
      imports: [HttpClientModule],
      providers: [AuthService, ApiService, baseAppUrl]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LogInPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
