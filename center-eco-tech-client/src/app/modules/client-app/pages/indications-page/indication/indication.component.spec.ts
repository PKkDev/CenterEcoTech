import { HttpClientModule } from '@angular/common/http';
import { StaticProvider } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ApiService } from 'src/app/services/api.service';

import { IndicationComponent } from './indication.component';

describe('IndicationComponent', () => {
  let component: IndicationComponent;
  let fixture: ComponentFixture<IndicationComponent>;

  const baseAppUrl: StaticProvider = { provide: 'BASE_APP_URL', useValue: '/', deps: [] };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientModule],
      declarations: [ IndicationComponent ],
      providers: [ApiService, baseAppUrl]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndicationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
