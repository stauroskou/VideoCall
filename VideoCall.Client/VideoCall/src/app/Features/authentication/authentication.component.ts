import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../Core/services/auth.service';

@Component({
  selector: 'app-authentication',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './authentication.component.html',
  styleUrl: './authentication.component.css'
})
export class AuthenticationComponent {
  authForm: FormGroup;
  isLoginMode = true;


  constructor(private fb: FormBuilder,
     private authService: AuthService,
     private router: Router) {
      
    this.authForm = this.fb.group({
      Username: ['', [Validators.required]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['']
    });
  }

  toggleMode() {
    this.isLoginMode = !this.isLoginMode;
    if (this.isLoginMode) {
      this.authForm.removeControl('confirmPassword');
    } else {
      this.authForm.addControl('confirmPassword', this.fb.control('', Validators.required));
    }
  }

  onSubmit() {
    if (this.authForm.invalid) return;
    const formData = this.authForm.value;
    
    this.authService.login(formData["Username"], formData["password"]).subscribe((success) => {
      if (success) {
        this.router.navigate(['/']); // Navigate to home after successful login
      } else {
        alert('Login failed');
      }
    });
  }
}
