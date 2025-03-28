import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

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
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  sessions: Session[] = [];
  selectedSession: Session | null = null;

  constructor(
    private router: Router
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
    console.log('Creating new session');
    // Implement create session logic
  }
}
