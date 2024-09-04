import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

interface CreateCustomerResponse {
  customerId: string;
}

export interface CreateAccountResponse{
  accountName: string;
  clabe: string;
  balance: number;
}

interface SigninResponse {
  customerId: string;
  name: string;
  lastName: string;
  email: string;
  phone: string;
  accountNumber: string;
  clabe: string;
  balance: number;
}

interface BalanceResponse {
  amount: number
}

interface TransactionResponse {
  type: string;
  amount: number;
  createdAt: string; // ISO 8601 date format
}

interface ServiceResponse<T> {
  status: string;
  data: T;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule, MatCardModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'fintechapp';
  
  customer: SigninResponse | undefined;
  transactions: TransactionResponse[] = [];

  showSigninForm: boolean = true;
  showRegistrationForm: boolean = false;
  showMainContent: boolean = false;

  constructor(private http: HttpClient) { }

  registrationForm = new FormGroup({
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phone: new FormControl(''),
    password: new FormControl('')
  });

  signinForm = new FormGroup({
    user: new FormControl(''),
    password: new FormControl(''),
  });

  depositForm = new FormGroup({
    amount: new FormControl('')
  });
  
  withdrawForm  = new FormGroup({
    amount: new FormControl('')
  });

  createAccount(customerId: string) {
    const body = { customerId: customerId };
    this.http.post("http://localhost:5284/api/v1/account", body).subscribe({
      next: (response) => console.log('Success:', response),
      error: (error) => console.error('Error:', error)
    });
  }

  getAllTransaction(customerId: string) {
    this.http.get<ServiceResponse<TransactionResponse[]>>(`http://localhost:5284/api/v1/transaction/customer/${customerId}`).subscribe({
      next: (response) => this.transactions = response.data,
      error: (error) => console.error('Error:', error)
    });
  }

  onSubmitRegistration() {
    this.http.post<ServiceResponse<CreateCustomerResponse>>('http://localhost:5284/api/v1/customer', this.registrationForm.value).subscribe({
      next: (response) => {
        this.createAccount(response.data.customerId);
        this.showRegistrationForm = false;
        this.showSigninForm = true;
      },
      error: (error) => console.error('Error:', error)
    });
  }

  onSubmitSignin() {
    this.http.post<ServiceResponse<SigninResponse>>('http://localhost:5284/api/v1/signin', this.signinForm.value).subscribe({
      next: (response) => {
        this.customer = response.data;
        this.getAllTransaction(response.data.customerId);
        this.showSigninForm = false;
        this.showMainContent = true; 
      },
      error: (error) => console.error('Error:', error)
    });
  }

  toggleLoginRegistration() {
    this.showSigninForm = !this.showSigninForm;
    this.showRegistrationForm = !this.showRegistrationForm;
  }
  
  logout() {
    this.showSigninForm = true;
    this.showMainContent = false;
    this.customer = undefined; // Reset customer information
  }

  onSubmitDeposit() {
    if (!this.customer) {
      console.error('Customer information is not available.');
      return;
    }
    const body = {
      ...this.depositForm.value,
      customerId: this.customer.customerId
    };
    this.http.post<ServiceResponse<BalanceResponse>>('http://localhost:5284/api/v1/transaction/deposit', body).subscribe({
      next: (response) => {
        if (!this.customer) {
          console.error('Customer information is not available.');
          return;
        }
        this.customer.balance = response.data.amount
      },
      error: (error) => console.error('Error:', error)
    });
  }

  onSubmitWithdraw() {
    if (!this.customer) {
      console.error('Customer information is not available.');
      return;
    }
    const body = {
      ...this.withdrawForm.value,
      customerId: this.customer.customerId
    };
    this.http.post<ServiceResponse<BalanceResponse>>('http://localhost:5284/api/v1/transaction/withdraw', body).subscribe({
      next: (response) => {
        if (!this.customer) {
          console.error('Customer information is not available.');
          return;
        }
        this.customer.balance = response.data.amount
      },
      error: (error) => console.error('Error:', error)
    });
  }
}
