import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormControl, FormGroup } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { CreateCustomerResponse, SigninResponse, TransactionResponse, ServiceResponse } from './models'; // Import models

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'fintechapp';

  registrationForm: FormGroup = new FormGroup({
    name: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl(''),
    phone: new FormControl(''),
    password: new FormControl('')
  });

  signinForm: FormGroup = new FormGroup({
    user: new FormControl(''),
    password: new FormControl('')
  });

  depositForm: FormGroup = new FormGroup({
    amount: new FormControl('')
  });

  withdrawForm: FormGroup = new FormGroup({
    amount: new FormControl('')
  });

  // State
  customer?: SigninResponse;
  transactions: TransactionResponse[] = [];
  showSigninForm = true;
  showRegistrationForm = false;
  showMainContent = false;

  constructor(private http: HttpClient) {}

  // ------------------ Account Methods ------------------

  /**
   * Creates a new account for the customer.
   */
  private createAccount(customerId: string) {
    const body = { customerId };
    this.http.post("http://localhost:5284/api/v1/account", body).subscribe({
      next: () => console.log('Account creation successful'),
      error: (error) => console.error('Error:', error)
    });
  }

  /**
   * Fetches all transactions for the given customer.
   */
  private getAllTransactions(customerId: string) {
    if (!this.customer?.token) return;
  
    const headers = { Authorization: `Bearer ${this.customer.token}` };
  
    this.http.get<ServiceResponse<TransactionResponse[]>>(
      `http://localhost:5284/api/v1/transaction/customer/${customerId}`,
      { headers }
    ).subscribe({
      next: (response) => this.transactions = response.data,
      error: (error) => console.error('Error:', error)
    });
  }

  // ------------------ Form Submit Handlers ------------------

  /**
   * Handles the registration form submission.
   */
  onSubmitRegistration() {
    this.http.post<ServiceResponse<CreateCustomerResponse>>('http://localhost:5284/api/v1/customer', this.registrationForm.value).subscribe({
      next: (response) => {
        this.createAccount(response.data.customerId);
        this.toggleLoginRegistration();
        this.registrationForm.reset();
      },
      error: (error) => console.error('Error:', error)
    });
  }

  /**
   * Handles the sign-in form submission.
   */
  onSubmitSignin() {
    this.http.post<ServiceResponse<SigninResponse>>('http://localhost:5284/api/v1/signin', this.signinForm.value).subscribe({
      next: (response) => {
        this.customer = response.data;
        this.getAllTransactions(response.data.customerId);
        this.showSigninForm = false;
        this.showMainContent = true;
      },
      error: (error) => console.error('Error:', error)
    });
  }

  /**
   * Handles the deposit form submission.
   */
  onSubmitDeposit() {
    if (!this.customer || !this.customer.token) return;
  
    const headers = { Authorization: `Bearer ${this.customer.token}` };
    const body = { ...this.depositForm.value, customerId: this.customer.customerId };
  
    this.http.post<ServiceResponse<TransactionResponse>>(
      'http://localhost:5284/api/v1/transaction/deposit',
      body,
      { headers }
    ).subscribe({
      next: (response) => {
        if(this.customer != null) {
          this.customer.balance += response.data.amount;
          this.transactions.unshift(response.data);
          this.depositForm.reset();
        }
      },
      error: (error) => console.error('Error:', error)
    });
  }

  /**
   * Handles the withdraw form submission.
   */
  onSubmitWithdraw() {
    if (!this.customer || !this.customer.token) return;
  
    const withdrawAmount = this.withdrawForm.get('amount')?.value;
  
    if (withdrawAmount > this.customer.balance) {
      console.error('Withdrawal amount exceeds balance');
      return;
    }
  
    const headers = { Authorization: `Bearer ${this.customer.token}` };
    const body = { ...this.withdrawForm.value, customerId: this.customer.customerId };
  
    this.http.post<ServiceResponse<TransactionResponse>>(
      'http://localhost:5284/api/v1/transaction/withdraw',
      body,
      { headers }
    ).subscribe({
      next: (response) => {
        if(this.customer != null) {
          this.customer.balance -= response.data.amount;
          this.transactions.unshift(response.data);
          this.withdrawForm.reset();
        }
      },
      error: (error) => console.error('Error:', error)
    });
  }

  // ------------------ Utility Methods ------------------

  /**
   * Toggles between login and registration forms.
   */
  toggleLoginRegistration() {
    this.showSigninForm = !this.showSigninForm;
    this.showRegistrationForm = !this.showRegistrationForm;
  }

  /**
   * Logs out the user and resets the customer information.
   */
  logout() {
    this.customer = undefined;
    this.showSigninForm = true;
    this.showMainContent = false;
  }
}
