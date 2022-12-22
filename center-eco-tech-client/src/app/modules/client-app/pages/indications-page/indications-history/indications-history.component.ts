import { AfterViewInit, Component, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/modules/authorize/services/auth.service';
import { ApiService } from 'src/app/services/api.service';
import { MeasurementRequestDto, NameCounter } from './domain';

@Component({
  selector: 'app-indications-history',
  templateUrl: './indications-history.component.html',
  styleUrls: ['./indications-history.component.scss']
})

export class IndicationsHistoryComponent implements AfterViewInit, OnDestroy {

  // filter
  public allTypes: NameCounter[] = [NameCounter.Hotwater, NameCounter.Coldwater, NameCounter.Gas];
  public selectedTypes: NameCounter | null = null;
  // date
  public selectedDate: Date | null = null;
  // data
  public MeasurementRequestDto: MeasurementRequestDto[] = [];
  // http
  private userRequestsSubs: Subscription;

  displayedColumns: string[] = ['date', 'name', 'pastvalue', 'currentvalue', 'cLientName', 'cLientAdress'];
  dataSource: MatTableDataSource<MeasurementRequestDto> = new MatTableDataSource();

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
    private apiService: ApiService) {
  }

  ngAfterViewInit() {
    this.getRequests();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy() {
    if (this.userRequestsSubs) this.userRequestsSubs.unsubscribe();
  }

  private getRequests() {
    const httpBody = {
      date: this.selectedDate ? this.formatDate(this.selectedDate) : null,
      types: this.selectedTypes
    };
    this.userRequestsSubs = this.apiService.post<MeasurementRequestDto[]>('measurement/history', httpBody)
      .subscribe({
        next: data => {
          this.MeasurementRequestDto = data;
          this.dataSource.data = this.MeasurementRequestDto;
        },
        error: error => {
          if (this.userRequestsSubs) this.userRequestsSubs.unsubscribe();
        },
        complete: () => { }
      });
  }
  private formatDate(date: Date): string {
    let dd = date.getDate();
    let mm = date.getMonth() + 1;
    return `${date.getFullYear()}-${mm < 10 ? '0' + mm : mm}-${dd < 10 ? '0' + dd : dd}`;
  }

  public onTypesSelected() {
    this.getRequests();
  }

  public onNewDateSelected() {
    this.getRequests();
  }

}
