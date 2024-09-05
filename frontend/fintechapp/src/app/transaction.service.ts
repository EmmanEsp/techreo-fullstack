import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionResponse, ServiceResponse } from './models';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Fetch all transactions for a given customer
  getAllTransactions(customerId: string, token: string): Observable<ServiceResponse<TransactionResponse[]>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    return this.http.get<ServiceResponse<TransactionResponse[]>>(
      `${this.apiUrl}/transaction/customer/${customerId}`, { headers }
    );
  }

  // Deposit a specified amount for a customer
  deposit(customerId: string, amount: number, token: string): Observable<ServiceResponse<TransactionResponse>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    const body = { customerId, amount };
    return this.http.post<ServiceResponse<TransactionResponse>>(
      `${this.apiUrl}/transaction/deposit`, body, { headers }
    );
  }

  // Withdraw a specified amount for a customer
  withdraw(customerId: string, amount: number, token: string): Observable<ServiceResponse<TransactionResponse>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    const body = { customerId, amount };
    return this.http.post<ServiceResponse<TransactionResponse>>(
      `${this.apiUrl}/transaction/withdraw`, body, { headers }
    );
  }
}
