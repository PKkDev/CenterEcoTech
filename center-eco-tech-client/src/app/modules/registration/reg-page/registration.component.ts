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
      name : "Cooperation1"
    },
    {
      id : 2,
      name : "Cooperation2"
    },
    {
      id : 3,
      name : "Cooperation3"
    }
  ]



  public onNextCLick() {
  }

}
