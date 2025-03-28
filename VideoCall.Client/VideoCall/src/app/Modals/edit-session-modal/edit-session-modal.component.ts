import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
  selector: 'app-edit-session-modal',
  templateUrl: './edit-session-modal.component.html',
  styleUrls: ['./edit-session-modal.component.css'],
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
export class EditSessionModalComponent {
  editForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<EditSessionModalComponent>,
    private fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    const session = data.session;
    this.editForm = this.fb.group({
      name: [session.name, Validators.required],
      date: [session.date || '', Validators.required], // Add date field
      startTime: [session.startTime || '', Validators.required],
      endTime: [session.endTime || '', Validators.required]
    });
  }

  onSubmit() {
    if (this.editForm.valid) {
      this.dialogRef.close(this.editForm.value); // Return updated session data
    }
  }

  onCancel() {
    this.dialogRef.close();
  }
}
