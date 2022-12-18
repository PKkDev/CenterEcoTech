import { AfterViewInit, Component, ViewChild, OnDestroy, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';
import { UserDetailDto } from './domain';

export interface UserData {
  Date: string;
  Status: string;
  Theme: string;
  FIO: string;
  Adress: string;
}

@Component({
  selector: 'app-applications-history',
  templateUrl: './applications-history.component.html',
  styleUrls: ['./applications-history.component.scss']
})

export class ApplicationsHistoryComponent implements AfterViewInit, OnDestroy {

  // data
  public userDetaill: UserDetailDto;
  // http
  private userDetailSubs: Subscription;
  private updateDetailSubs: Subscription;
  private ConfirmUserSubs: Subscription;

  displayedColumns: string[] = ['Date', 'Status', 'Theme', 'FIO', 'Adress'];
  dataSource: MatTableDataSource<UserData>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  pageSize = 10;
  pageIndex = 0;
  hidePageSize = true;
  showPageSizeOptions = false;
  showFirstLastButtons = true;
  disabled = false;

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  constructor(
    protected authService: AuthService,
    private router: Router,
    private aiService: ApiService) {

    // Assign the data to the data source for the table to render
    this.dataSource = new MatTableDataSource();
  }

  ngAfterViewInit() {
    this.getTableDetail();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.userDetailSubs) this.userDetailSubs.unsubscribe();
    if (this.updateDetailSubs) this.updateDetailSubs.unsubscribe();
    if (this.ConfirmUserSubs) this.ConfirmUserSubs.unsubscribe();
  }

  private getTableDetail() {
    this.userDetailSubs = this.aiService.get<UserDetailDto>('client/detail')
      .subscribe({
        next: data => { this.userDetaill = data; },
        error: error => { if (this.userDetailSubs) this.userDetailSubs.unsubscribe(); },
        complete: () => { }
      });
  }

}
