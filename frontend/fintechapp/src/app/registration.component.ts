import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth.service'; 
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatProgressSpinnerModule,
    CommonModule
  ],
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
    @Output() registrationSuccess = new EventEmitter<void>();

    isLoading = false; // Track loading state
    isSuccess = false; // Track success state
    errorMessage: string | null = null; // Track error messages

    registrationForm: FormGroup = new FormGroup({
        name: new FormControl('', [Validators.required, Validators.minLength(2)]),
        lastName: new FormControl('', [Validators.required, Validators.minLength(2)]),
        email: new FormControl('', [Validators.required, Validators.email]),
        phone: new FormControl('', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]),
        password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });

    constructor(private authService: AuthService, private snackBar: MatSnackBar) { }

    toggleLoginRegistration() {
        this.registrationSuccess.emit();
    }

    // Registration form submit method
    onSubmitRegistration() {
        const registrationData = {
            ...this.registrationForm.value,
            phone: this.registrationForm.value.phone.toString(),
        };
        
        if (this.registrationForm.valid) {
            this.isLoading = true;
            this.authService.registerCustomer(registrationData).subscribe({
            next: (response) => {
                this.isLoading = false;
                this.isSuccess = true;
                this.showSuccessMessage();
                this.registrationForm.reset();
                this.toggleLoginRegistration();
            },
            error: (error: HttpErrorResponse) => {
                this.handleErrorMessage(error);
                this.isLoading = false;
            },
            });
        } else {
            this.registrationForm.markAllAsTouched();
        }
    }

    // Handle error messages based on server response
    private handleErrorMessage(error: HttpErrorResponse) {
        if (error.error?.message) {
            this.showErrorMessage(error.error.message);
        } else {
            this.showErrorMessage("Error en el servido. Intentelo mas tarde.");
        }
    }

    // Show success message using Angular Material Snackbar
    showSuccessMessage() {
        this.snackBar.open('Registro exitoso. ¡Bienvenido!', '', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
        });
    }

    // Show error message using Angular Material Snackbar
    showErrorMessage(errorMessage: string) {
        this.snackBar.open(errorMessage, '', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom'
        });
    }
}
