import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searching: boolean = false;
  searchQuery: string = "";

  @Output() searchEvent = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  searchClick() {
    this.searching = true;
    this.searchEvent.emit(this.searchQuery);
  }

  public searchFinished() {
    this.searching = false;
  }

}
