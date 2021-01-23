import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';


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

  public getAll(searchQuery: string, offset: number, next: number): Observable<any[]> {
    console.log(`looking for ${searchQuery}, offset: ${offset}`);
    const params = new HttpParams().set("searchQuery", searchQuery)
      .set("offset", offset.toString())
      .set("next", next.toString());

    return this.httpClient.get<any[]>(this.SERVICE_URL, { params });
  }
}
