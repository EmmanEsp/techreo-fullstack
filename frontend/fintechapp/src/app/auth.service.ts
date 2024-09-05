import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SigninResponse, ServiceResponse, CreateCustomerResponse } from './models';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'token';

  constructor(private http: HttpClient) {}

  // Save token to localStorage
  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  // Get token from localStorage
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // Clear token from localStorage
  clearToken() {
    localStorage.removeItem(this.tokenKey);
  }

  // Register a new customer
  registerCustomer(data: any): Observable<ServiceResponse<CreateCustomerResponse>> {
    return this.http.post<ServiceResponse<CreateCustomerResponse>>(
      'http://localhost:5284/api/v1/customer', data
    );
  }

  // Sign in a customer and retrieve the session data
  signIn(user: string, password: string): Observable<ServiceResponse<SigninResponse>> {
    return this.http.post<ServiceResponse<SigninResponse>>(
      'http://localhost:5284/api/v1/signin', { user, password }
    );
  }

  // Get session data using the stored token
  getSessionData(): Observable<ServiceResponse<SigninResponse>> {
    const token = this.getToken();
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    return this.http.get<ServiceResponse<SigninResponse>>(
      'http://localhost:5284/api/v1/signin/session-data', { headers }
    );
  }
}
