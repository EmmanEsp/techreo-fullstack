import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service'; // Import AuthService
import { TransactionService } from './transaction.service'; // Import TransactionService
import { SigninResponse, TransactionResponse } from './models'; // Import the models

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    CommonModule
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'fintechapp';

  // Forms for registration, sign-in, deposit, withdraw
  registrationForm: FormGroup = new FormGroup({
    name: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    email: new FormControl('', [Validators.required, Validators.email]),
    phone: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });
  
  signinForm: FormGroup = new FormGroup({
    user: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required)
  });

  depositForm: FormGroup = new FormGroup({
    amount: new FormControl('', Validators.required)
  });

  withdrawForm: FormGroup = new FormGroup({
    amount: new FormControl('', Validators.required)
  });

  // State variables
  customer?: SigninResponse;
  transactions: TransactionResponse[] = [];
  showSigninForm = true;
  showRegistrationForm = false;
  showMainContent = false;

  constructor(
    private authService: AuthService,
    private transactionService: TransactionService
  ) {}

  ngOnInit() {
    const token = this.authService.getToken();
    if (token) {
      this.authService.getSessionData().subscribe({
        next: (response) => {
          this.customer = response.data;
          this.showSigninForm = false;
          this.showMainContent = true;
          this.getAllTransactions();
        },
        error: (error) => console.error('Error:', error)
      });
    }
  }

  // Registration form submit method
  onSubmitRegistration() {
    this.authService.registerCustomer(this.registrationForm.value).subscribe({
      next: (response) => {
        this.toggleLoginRegistration();
        this.registrationForm.reset();
      },
      error: (error) => console.error('Error:', error)
    });
  }

  // Sign in form submit method
  onSubmitSignin() {
    const { user, password } = this.signinForm.value;
    this.authService.signIn(user, password).subscribe({
      next: (response) => {
        this.customer = response.data;
        this.authService.setToken(response.data.token);
        this.getAllTransactions();
        this.showSigninForm = false;
        this.showMainContent = true;
      },
      error: (error) => console.error('Error:', error)
    });
  }

  // Fetch all transactions
  private getAllTransactions() {
    if (!this.customer || !this.authService.getToken()) return;
    this.transactionService.getAllTransactions(this.customer.customerId, this.authService.getToken()!)
      .subscribe({
        next: (response) => this.transactions = response.data,
        error: (error) => console.error('Error fetching transactions:', error)
      });
  }

  // Deposit form submit method
  onSubmitDeposit() {
    if (this.depositForm.valid && this.customer && this.authService.getToken()) {
      const { amount } = this.depositForm.value;
      this.transactionService.deposit(this.customer.customerId, amount, this.authService.getToken()!)
        .subscribe({
          next: (response) => {
            this.customer!.balance += response.data.amount;
            this.transactions.unshift(response.data);
            this.depositForm.reset();
          },
          error: (error) => console.error('Error during deposit:', error)
        });
    }
  }

  // Withdraw form submit method
  onSubmitWithdraw() {
    if (this.withdrawForm.valid && this.customer && this.authService.getToken()) {
      const { amount } = this.withdrawForm.value;
      if (amount > this.customer!.balance) {
        console.error('Withdrawal amount exceeds balance');
        return;
      }

      this.transactionService.withdraw(this.customer.customerId, amount, this.authService.getToken()!)
        .subscribe({
          next: (response) => {
            this.customer!.balance -= response.data.amount;
            this.transactions.unshift(response.data);
            this.withdrawForm.reset();
          },
          error: (error) => console.error('Error during withdrawal:', error)
        });
    }
  }

  // Toggle between registration and sign-in forms
  toggleLoginRegistration() {
    this.showSigninForm = !this.showSigninForm;
    this.showRegistrationForm = !this.showRegistrationForm;
  }

  // Logout and clear session
  logout() {
    this.authService.clearToken();
    this.customer = undefined;
    this.showSigninForm = true;
    this.showMainContent = false;
  }
}
