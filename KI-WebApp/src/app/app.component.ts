import { Component, ViewChild } from '@angular/core';
import { SearchBarComponent } from './search-bar/search-bar.component';
import { SearchResultsComponent } from './search-results/search-results.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @ViewChild(SearchBarComponent) searchBarChild: SearchBarComponent | undefined;
  @ViewChild(SearchResultsComponent) searchResultsChild: SearchResultsComponent | undefined;

  searchEvent($event: string) {
    this.searchResultsChild?.search($event);
  }

  searchFinishedEvent() {
    this.searchBarChild?.searchFinished();
  }
}
