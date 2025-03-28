import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIcon, MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  selector: 'app-session-card',
  templateUrl: './session-card.component.html',
  styleUrls: ['./session-card.component.css'],
  imports: [MatIconModule, MatMenuModule, MatButtonModule,CommonModule], // Import necessary Angular modules here if needed
})
export class SessionCardComponent {
  @Input() session: any; // Input to receive session data
  @Input() selectedSession: any; // Input to track the selected session
  @Output() edit = new EventEmitter<void>(); // Output event for edit
  @Output() delete = new EventEmitter<void>(); // Output event for delete
  @Output() join = new EventEmitter<void>(); // Output event for join
  @Output() toggleDetails = new EventEmitter<void>(); // Output event for toggling details
}
