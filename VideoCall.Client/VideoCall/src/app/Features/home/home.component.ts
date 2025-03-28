import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { CreateSessionModalComponent } from '../../Modals/create-session-modal/create-session-modal.component'; 
import { AuthService } from '../../Core/services/auth.service'; // Import AuthService
import { EditSessionModalComponent } from '../../Modals/edit-session-modal/edit-session-modal.component'; // Import the edit modal component
import { MatButtonModule } from '@angular/material/button';
import { SessionCardComponent } from '../../Shared/session-card/session-card.component';

interface Session {
  id: string;
  name: string;
  participants: number;
  status: 'active' | 'waiting' | 'ended';
  createdAt: Date;
  endTime?: Date;
  host: string;
  description?: string;
  participantList: string[];
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule,
    MatToolbarModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    SessionCardComponent // Import the SessionCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  sessions: Session[] = [];
  selectedSession: Session | null = null;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private authService: AuthService // Inject AuthService
  ) {}

  ngOnInit() {
    // Temporary mock data - later replace with actual API call
    this.sessions = [
      {
        id: '1',
        name: 'Team Meeting',
        participants: 4,
        status: 'active',
        createdAt: new Date(),
        host: 'John Doe',
        description: 'Weekly team sync meeting',
        participantList: ['John Doe', 'Jane Smith', 'Bob Johnson', 'Alice Brown']
      },
      {
        id: '2',
        name: 'Project Discussion',
        participants: 2,
        status: 'waiting',
        createdAt: new Date(),
        host: 'Jane Smith',
        description: 'Project planning session',
        participantList: ['Jane Smith', 'Bob Johnson']
      }
    ];
  }

  showSessionDetails(session: Session) {
    this.selectedSession = this.selectedSession?.id === session.id ? null : session;
  }

  joinSession(sessionId: string) {
    console.log(`Joining session ${sessionId}`);
    this.router.navigate(['/Session', sessionId]);
  }

  createNewSession() {
    const dialogRef = this.dialog.open(CreateSessionModalComponent, {
      width: '600px', // Set the width of the modal
      data: {}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('New session created:', result);
        this.sessions.push(result); // Add the new session to the list
      }
    });
  }

  openEditSessionModal(session: Session, index: number) {
    const dialogRef = this.dialog.open(EditSessionModalComponent, {
      width: '600px',
      data: { session }
    });

    dialogRef.afterClosed().subscribe((updatedSession) => {
      if (updatedSession) {
        this.sessions[index] = { ...this.sessions[index], ...updatedSession }; // Update the session with new values
      }
    });
  }

  deleteSession(index: number) {
    const confirmDelete = confirm('Are you sure you want to delete this session?');
    if (confirmDelete) {
      this.sessions.splice(index, 1); // Remove the session from the list
    }
  }

  logout() {
    this.authService.logout(); // Call the logout method from AuthService
    this.router.navigate(['/Authentication']); // Redirect to the login page
  }
}
