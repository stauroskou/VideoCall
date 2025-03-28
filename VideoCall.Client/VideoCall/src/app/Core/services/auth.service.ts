import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { map, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);

  private apiUrl = 'https://localhost:7012/api/Account';
  private tokenKey = 'authToken'; // Key to store the JWT token in localStorage

  // Login method
  login(username: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap((response) => {
        if (response.success && response.data.token) {
          localStorage.setItem(this.tokenKey, response.data.token); // Store JWT token in localStorage
        }
      }),
      map((response) => !!response.success) // Return true if login was successful
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey); // Remove the JWT token from localStorage
    this.router.navigate(['/login']); // Redirect to login page
  }

  isAuthenticated(): boolean {
    const token = this.getAuthToken();
    return !!token && !this.isTokenExpired(token); // Check if the token exists and is not expired
  }

  getAuthToken(): string | null {
    return localStorage.getItem(this.tokenKey); // Retrieve the JWT token from localStorage
  }

  private isTokenExpired(token: string): boolean {
    try {
      const payload = JSON.parse(atob(token.split('.')[1])); // Decode the JWT payload
      const expiry = payload.exp * 1000; // Convert expiry time to milliseconds
      return Date.now() > expiry; // Check if the token is expired
    } catch (e) {
      return true; // If decoding fails, consider the token expired
    }
  }

  constructor() {}
}
