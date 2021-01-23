import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { OrderDetailsService } from '../order-details.service';

@Component({
  selector: 'app-search-results',
  templateUrl: './search-results.component.html',
  styleUrls: ['./search-results.component.css']
})
export class SearchResultsComponent implements OnInit {

  searching: boolean = false;
  offset: number = 0;
  next: number = 10;
  orderDetailData: any;
  rowCount: number = 0;

  searchQuery: string = "";

  currentPage: number = 0;
  pageCount: number = 0;

  error: boolean = false;
  errorMessage: string = "";

  @Output() searchFinishedEvent = new EventEmitter();

  constructor(public orderDetailsService: OrderDetailsService) { }

  ngOnInit(): void {
  }

  public previousPage() {
    this.search(this.searchQuery, (this.offset - this.next));
  }

  public nextPage() {
    this.search(this.searchQuery, (this.offset + this.next));
  }

  public search(searchQuery: string, offset: number = 0) {
    this.offset = offset;
    this.searchQuery = searchQuery;
    this.searching = true;
    this.orderDetailData = null;
    this.error = false;

    this.orderDetailsService.getAll(searchQuery, offset, this.next)
      .pipe(
        catchError(async (error) => this.errorHandler(error))
      )
      .subscribe((data: any) => {
        console.log(data);
        if (data != undefined) {
          this.rowCount = data.rowCount;
          this.currentPage = (offset / this.next) + 1;
          this.pageCount = Math.ceil(this.rowCount / this.next);
          this.orderDetailData = data.items;
        }
        this.searching = false;
        this.searchFinishedEvent.emit();
      });
  }

  public errorHandler(errorObject: HttpErrorResponse) {
    console.log("error");
    this.error = true;
    this.errorMessage = `Status code ${errorObject.status}: ${errorObject.error}`;
    this.searching = false;
    this.searchFinishedEvent.emit();
  }

}
