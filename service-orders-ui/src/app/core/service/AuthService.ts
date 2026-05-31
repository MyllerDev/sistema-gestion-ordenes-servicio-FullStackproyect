import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api =
    'https://localhost:5001/api/auth';

  constructor(
    private http: HttpClient
  ) { }

  login(data: any): Observable<any> {
    return this.http.post(
      `${this.api}/login`,
      data
    );
  }

  saveToken(token: string): void {
    localStorage.setItem(
      'token',
      token
    );
  }

  getToken(): string | null {
    return localStorage.getItem(
      'token'
    );
  }

  logout(): void {
    localStorage.clear();
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}