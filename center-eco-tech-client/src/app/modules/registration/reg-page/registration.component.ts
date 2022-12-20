import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  // field
  public phone: string;
  public coops = [
    {
      id : 1,
      name : "Cooperative1"
    },
    {
      id : 2,
      name : "Cooperative2"
    },
    {
      id : 3,
      name : "Cooperative3"
    }
  ]

  public onNextCLick() {
  }

}
