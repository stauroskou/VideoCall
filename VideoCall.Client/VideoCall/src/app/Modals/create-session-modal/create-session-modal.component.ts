import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-create-session-modal',
  templateUrl: './create-session-modal.component.html',
  styleUrls: ['./create-session-modal.component.css'],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule
  ],
  providers: [  
    MatDatepickerModule,
    MatNativeDateModule   
  ],
})
export class CreateSessionModalComponent {
  sessionForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<CreateSessionModalComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.sessionForm = this.fb.group({
      name: ['', Validators.required],
      date: ['', Validators.required], // New date field
      startTime: ['', Validators.required],
      endTime: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.sessionForm.valid) {
      const newSession = {
        id: Math.random().toString(36).substr(2, 9), // Generate a random ID
        name: this.sessionForm.value.name,
        participants: 0,
        status: 'waiting',
        createdAt: new Date(),
        date: this.sessionForm.value.date, // Date of the session
        startTime: this.sessionForm.value.startTime,
        endTime: this.sessionForm.value.endTime,
        participantList: []
      };
      this.dialogRef.close(newSession);
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}