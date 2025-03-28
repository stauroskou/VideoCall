import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

interface Participant {
  id: string;
  name: string;
  isHost: boolean;
  isMuted: boolean;
  isVideoOn: boolean;
}

@Component({
  selector: 'app-session',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './session.component.html',
  styleUrl: './session.component.css'
})
export class SessionComponent implements OnInit {
  sessionId: string | null = null;
  participants: Participant[] = [];
  isHost: boolean = false;
  isMuted: boolean = false;
  isVideoOn: boolean = true;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit() {
    this.sessionId = this.route.snapshot.paramMap.get('id');
    if (!this.sessionId) {
      this.router.navigate(['/home']);
      return;
    }
    // Mock data - replace with actual API calls
    this.participants = [
      { id: '1', name: 'You', isHost: true, isMuted: false, isVideoOn: true },
      { id: '2', name: 'John Doe', isHost: false, isMuted: true, isVideoOn: true },
      { id: '3', name: 'Jane Smith', isHost: false, isMuted: false, isVideoOn: false }
    ];
  }

  toggleMute() {
    this.isMuted = !this.isMuted;
  }

  toggleVideo() {
    this.isVideoOn = !this.isVideoOn;
  }

  leaveSession() {
    this.router.navigate(['/Home']);
  }

  endSession() {
    // Implement end session logic for host
    console.log('Ending session for all participants');
  }
}
