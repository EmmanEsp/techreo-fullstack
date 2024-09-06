import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service';
import { TransactionService } from './transaction.service';
import { SigninResponse, TransactionResponse } from './models';
import { RegistrationComponent } from './registration.component'; 
import { MatSnackBar } from '@angular/material/snack-bar'; 
import { MatIconModule } from '@angular/material/icon';
import { TransactionTypePipe } from './transaction-type.pipe';

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
    CommonModule,
    RegistrationComponent,
    MatIconModule,
    TransactionTypePipe
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'fintechapp';

  // Forms for sign-in, deposit, withdraw
  signinForm: FormGroup = new FormGroup({
    user: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  depositForm: FormGroup = new FormGroup({
    amount: new FormControl('', [Validators.required, Validators.min(1)])
  });

  withdrawForm: FormGroup = new FormGroup({
    amount: new FormControl('', [Validators.required, Validators.min(1)])
  });

  // State variables
  customer?: SigninResponse;
  transactions: TransactionResponse[] = [];
  showSigninForm = true;
  showRegistrationForm = false;
  showMainContent = false;

  constructor(
    private authService: AuthService,
    private transactionService: TransactionService,
    private snackBar: MatSnackBar
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

  // Sign in form submit method
  onSubmitSignin() {
    if (!this.signinForm.valid) {
      this.signinForm.markAllAsTouched();
      return;
    }
    const { user, password } = this.signinForm.value;
    this.authService.signIn(user, password).subscribe({
      next: (response) => {
        this.customer = response.data;
        this.authService.setToken(response.data.token);
        this.getAllTransactions();
        this.showSigninForm = false;
        this.showMainContent = true;
        this.signinForm.reset();
      },
      error: (error) => {
        console.error('Error:', error);
        this.snackBar.open(`Las credenciales no coinciden con ningún usuario.`, '', {
          duration: 3000,
          panelClass: ['snackbar-error'],
          horizontalPosition: 'center',
          verticalPosition: 'bottom'
        });
      }
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
            this.snackBar.open(`Se ha depositado a tu cuenta $${amount} con éxito.`, '', {
              duration: 3000,
              panelClass: ['snackbar-error'],
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            });
          },
          error: (error) => console.error('Error during deposit:', error)
        });
    }
  }

  // Withdraw form submit method
  onSubmitWithdraw() {
    if (this.withdrawForm.value.amount <= 0) {
      this.withdrawForm.markAllAsTouched();
      return;
    }
    if (this.withdrawForm.valid && this.customer && this.authService.getToken()) {
      const { amount } = this.withdrawForm.value;
      if (amount > this.customer!.balance) {
        console.error('Withdrawal amount exceeds balance');
        this.snackBar.open('No tienes los fondos suficientes', '', {
          duration: 3000,
          panelClass: ['snackbar-error'],
          horizontalPosition: 'center',
          verticalPosition: 'bottom'
        });
        this.withdrawForm.reset();
        return;
      }

      this.transactionService.withdraw(this.customer.customerId, amount, this.authService.getToken()!)
        .subscribe({
          next: (response) => {
            this.customer!.balance -= response.data.amount;
            this.transactions.unshift(response.data);
            this.withdrawForm.reset();
            this.snackBar.open(`Se ha retirado de tu cuenta $${response.data.amount} con éxito.`, '', {
              duration: 3000,
              panelClass: ['snackbar-error'],
              horizontalPosition: 'center',
              verticalPosition: 'bottom'
            });
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

  validateAmount(event: any) {
    const input = event.target;
    let value = input.value;

    // Prevent negative values
    if (value < 0) {
      input.value = '';
      return;
    }

    // Check if the value already has a decimal part and limit it to 2 decimal places
    if (value.includes('.')) {
      const [integerPart, decimalPart] = value.split('.');
      if (decimalPart.length > 2) {
        input.value = `${integerPart}.${decimalPart.slice(0, 2)}`; // Keep only the first two decimals
      }
    }
  }
}
