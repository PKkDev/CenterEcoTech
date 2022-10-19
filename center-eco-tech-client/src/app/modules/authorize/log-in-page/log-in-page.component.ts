import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-log-in-page',
  templateUrl: './log-in-page.component.html',
  styleUrls: ['./log-in-page.component.scss']
})
export class LogInPageComponent implements OnInit, OnDestroy {

  constructor(
    private router: Router,
    private authService: AuthService) { }

  ngOnInit() { }

  ngOnDestroy() { }

}
