import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SigninResponse, ServiceResponse, CreateCustomerResponse } from './models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private tokenKey = 'token';
  private apiUrl = environment.apiUrl;

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

  // Register a new customer with error handling
  registerCustomer(data: any): Observable<ServiceResponse<CreateCustomerResponse>> {
    return this.http.post<ServiceResponse<CreateCustomerResponse>>(
      `${this.apiUrl}/customer`, data
    );
  }

  // Sign in a customer with error handling
  signIn(user: string, password: string): Observable<ServiceResponse<SigninResponse>> {
    return this.http.post<ServiceResponse<SigninResponse>>(
      `${this.apiUrl}/signin`, { user, password }
    );
  }

  // Get session data with error handling
  getSessionData(): Observable<ServiceResponse<SigninResponse>> {
    const token = this.getToken();
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    return this.http.post<ServiceResponse<SigninResponse>>(
      `${this.apiUrl}/signin/session-data`, {}, { headers }
    );
  }
}
