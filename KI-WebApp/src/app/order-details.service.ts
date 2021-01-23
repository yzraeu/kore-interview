import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderDetails } from './entities/OrderDetails';

@Injectable({
  providedIn: 'root'
})
export class OrderDetailsService {

  private SERVICE_URL: string = "https://localhost:5001/OrderDetails";

  data = [{
    productName: "AWC Logo Cap",
    productNumber: "CA-1098"
  }];

  constructor(private httpClient: HttpClient) { }

  public getAllMock(): Array<{ productName: string, productNumber: string }> {
    return this.data;
  }

  public getAll(searchQuery: string, offset: number, next: number): Observable<OrderDetails[]> {
    console.log('looking for: ' + searchQuery);
    const params = new HttpParams().set("searchQuery", searchQuery)
      .set("offset", offset.toString())
      .set("next", next.toString());

    return this.httpClient.get<OrderDetails[]>(this.SERVICE_URL, { params });
  }
}
