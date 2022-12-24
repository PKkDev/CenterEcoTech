import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-news-page',
  templateUrl: './news-page.component.html',
  styleUrls: ['./news-page.component.scss']
})
export class NewsPageComponent implements OnInit {

  constructor(private httpClient: HttpClient){}

  public articles: any = [];

  ngOnInit(){
    this.httpClient.get("assets/articles/articles.json").subscribe({
      next : data =>{
      this.articles = data;
      }
    })
  }

  

}