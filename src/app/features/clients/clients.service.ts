import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ClientsService {

  private apiUrl = 'https://localhost:5001/api/clients';

  constructor(private http: HttpClient) {}

  getClients(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  getClientById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/${id}`);
  }

  createClient(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  updateClient(id: number, data: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, data);
  }

  deleteClient(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}