import { Component, OnInit } from '@angular/core';
import { OrderDetails } from '../entities/OrderDetails';
import { OrderDetailsService } from '../order-details.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searchButtonText: string = "Search";
  searching = false;
  searchQuery = "";
  offset: number = 0;
  next: number = 20;
  data: any;

  constructor(public orderDetailsService: OrderDetailsService) { }

  ngOnInit(): void {
  }

  clicked = false;

  searchClick() {
    this.searching = true;
    this.searchButtonText = "Wait...";

    this.orderDetailsService.getAll(this.searchQuery, this.offset, this.next).subscribe((data: OrderDetails[]) => {
      console.log(data);
      this.data = data;
      this.searching = false;
      this.searchButtonText = "Search";
    });
  }

}
