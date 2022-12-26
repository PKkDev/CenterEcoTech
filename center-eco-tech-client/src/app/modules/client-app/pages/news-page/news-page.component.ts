import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, OnInit, TemplateRef, ViewChild } from '@angular/core';

@Component({
  selector: 'app-news-page',
  templateUrl: './news-page.component.html',
  styleUrls: ['./news-page.component.scss']
})
export class NewsPageComponent implements OnInit {

  // template
  @ViewChild('allNewsTemplate') allNewsTemplate: TemplateRef<any>;
  @ViewChild('articleTemplate') articleTemplate: TemplateRef<any>;
  public nowTemplate: TemplateRef<any>;
  // data
  public articles: any = [];
  public currArticle: any = [];

  constructor(
    private cdr: ChangeDetectorRef,
    private httpClient: HttpClient) { }

  ngOnInit() {
    this.httpClient.get("assets/articles/articles.json")
      .subscribe({
        next: data => {
          this.articles = data;
        }
      })
  }

  ngAfterViewInit() {
    this.onLoadTemplate();
  }

  public onLoadTemplate() {
    switch (this.currArticle.length) {
      case (0): {
        this.nowTemplate = this.allNewsTemplate;
        break;
      }
      case (1): {
        this.nowTemplate = this.articleTemplate;
        break;
      }
      default: this.nowTemplate = this.allNewsTemplate; break;
    }
    this.cdr.detectChanges();
  }

  public setArticle(currPic: string, currName: string, currContent: string) {
    this.currArticle.push({
      "pic": currPic,
      "name": currName,
      "content": currContent
    });
    this.onLoadTemplate();
  }

  onBackClick() {
    this.currArticle = [];
    this.nowTemplate = this.allNewsTemplate;
  }

}