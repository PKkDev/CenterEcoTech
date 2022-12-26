import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-indication',
  templateUrl: './indication.component.html',
  styleUrls: ['./indication.component.scss']
})
export class IndicationComponent implements OnInit {

  public meters: any = [];
  public currImg: string;
  public images: any = [];

  constructor(private cdr: ChangeDetectorRef,
    private httpClient: HttpClient){}

  ngOnInit(): void {
   
    this.httpClient.get("assets/meters.json").subscribe({
      next : data =>{
      this.meters = data
      }
    });

    }
  }
