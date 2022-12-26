import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { clientCounterDto } from './domain';
import { ApiService } from 'src/app/services/api.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-indication',
  templateUrl: './indication.component.html',
  styleUrls: ['./indication.component.scss']
})
export class IndicationComponent implements OnInit {

  public clientCounterDto: clientCounterDto[] = [];
  public currImg: string;
  public images: any = [];


  private addMeterSups: Subscription;
  private lastMeasureSups: Subscription;
  public adding: boolean = false;
  public selectedMeterName: string = "Горячая вода";
  public message: string | null;

  constructor(private cdr: ChangeDetectorRef,
    private httpClient: HttpClient,
    private apiService: ApiService){}

  ngOnInit(): void {
  //  this.getLastMeasurement();
    this.httpClient.get<clientCounterDto[]>("assets/meters.json").subscribe({
      next : data =>{
      this.clientCounterDto = data
      }
    });
    }

  public activateAddingMeter(): void{
    this.adding = true;
  }

  private getLastMeasurement() {
    this.lastMeasureSups = this.apiService.post('measurement/get-last-measurement')
    .subscribe({
      next: data => { 
        console.log(data);
      },
          error: error => { if (this.lastMeasureSups) this.lastMeasureSups.unsubscribe(); this.message = error.error; },
          complete: () => { }
    })
  }

  private addMeter() {
    this.message = null;
    const httpBody = {
      name : this.selectedMeterName
    };
    this.addMeterSups = this.apiService.post('measurement/add-new-counter', httpBody)
    .subscribe({
      next: data => { },
          error: error => { if (this.addMeterSups) this.addMeterSups.unsubscribe(); this.message = error.error; },
          complete: () => { this.message = "Счётчик успешно добавлен" }
    })
  }

  public onAddCLick() {
    // this.adding = false;
    this.addMeter();
  }

  public onReadyCLick() {
    this.message = null;
    this.adding = false;
  }

  }
