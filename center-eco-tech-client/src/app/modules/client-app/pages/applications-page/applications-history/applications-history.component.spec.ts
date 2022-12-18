import { DatePipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';

import { ApplicationsHistoryComponent } from './applications-history.component';
import { ApplictionStatusConverterPipe } from './appliction-status-converter.pipe';

describe('ApplicationsHistoryComponent', () => {
  let component: ApplicationsHistoryComponent;
  let fixture: ComponentFixture<ApplicationsHistoryComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule, FormsModule, ReactiveFormsModule, MatSelectModule, MatFormFieldModule, MatInputModule, BrowserAnimationsModule],
      declarations: [ApplicationsHistoryComponent, ApplictionStatusConverterPipe],
      providers: [AuthService, ApiService, baseAppUrl]
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
