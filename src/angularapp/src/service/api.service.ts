import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  constructor(private http: HttpClient) { }
  

  calculateCdb(data: any) {
    const url = `${environment.apiBaseUrl}/cdbCalculation`; 
    return this.http.post(url, data);
  }
}
