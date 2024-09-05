import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TransactionResponse, ServiceResponse } from './models';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {

  constructor(private http: HttpClient) {}

  // Fetch all transactions for a given customer
  getAllTransactions(customerId: string, token: string): Observable<ServiceResponse<TransactionResponse[]>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    return this.http.get<ServiceResponse<TransactionResponse[]>>(
      `http://localhost:5284/api/v1/transaction/customer/${customerId}`, { headers }
    );
  }

  // Deposit a specified amount for a customer
  deposit(customerId: string, amount: number, token: string): Observable<ServiceResponse<TransactionResponse>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    const body = { customerId, amount };
    return this.http.post<ServiceResponse<TransactionResponse>>(
      'http://localhost:5284/api/v1/transaction/deposit', body, { headers }
    );
  }

  // Withdraw a specified amount for a customer
  withdraw(customerId: string, amount: number, token: string): Observable<ServiceResponse<TransactionResponse>> {
    const headers = new HttpHeaders({ Authorization: `Bearer ${token}` });
    const body = { customerId, amount };
    return this.http.post<ServiceResponse<TransactionResponse>>(
      'http://localhost:5284/api/v1/transaction/withdraw', body, { headers }
    );
  }
}
