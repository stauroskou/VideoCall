import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { inject } from '@angular/core';
import { map, Observable, of, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http = inject(HttpClient);
  private cookieService = inject(CookieService);
  private router = inject(Router);

  private apiUrl = 'https://localhost:44372/api/Account';


   // Login method
   login(username: string, password: string): Observable<boolean> {
    return this.http.post<any>(`${this.apiUrl}/login`, { username, password }, { withCredentials: true }).pipe(
      tap((response) => {
        console.log(response);
        if (response.success) {
          //this.cookieService.set('authToken', response.token, 1, '/'); // Store token in cookie (expires in 1 day)
        }
      }),
      map((response) => !!response.success) // Return true if login was successful
    );
  }

  logout(): void {
    this.cookieService.delete('.AspNetCore.Identity.Application', '/'); // Delete the cookie
    this.router.navigate(['/login']); // Redirect to login page
  }

  isAuthenticated(): boolean {
    var token = this.cookieService.getAll();
    
    return this.checkCookie(".AspNetCore.Identity.Application"); // Check if the auth token exists
  }

  getAuthToken(): string {
    return this.cookieService.get('.AspNetCore.Identity.Application');
  }


  checkCookie(name: string) {
    const cookie = document.cookie.split('; ').find(cookie => cookie.startsWith(name));
    console.log(document.cookie.split('; '));
    return cookie ? true : false;
  }
  constructor() { }
}
