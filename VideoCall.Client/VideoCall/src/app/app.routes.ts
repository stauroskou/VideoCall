import { Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthenticationComponent } from './Features/authentication/authentication.component';
import { HomeComponent } from './Features/home/home.component';
import { SessionComponent } from './Features/session/session.component';
import { authGuard } from './Core/guards/auth.guard';

export const routes: Routes = [
    { path: 'Home', component: HomeComponent, canActivate: [authGuard] },
    { path: 'Authentication', component: AuthenticationComponent },
    { path: 'Session/:id', component: SessionComponent, canActivate: [authGuard] },
    {path: '', redirectTo: '/Home', pathMatch: 'full'}
];
