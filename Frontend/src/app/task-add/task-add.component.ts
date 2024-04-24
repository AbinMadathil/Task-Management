import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-task-add',
  templateUrl: './task-add.component.html',
  styleUrl: './task-add.component.css'
})
export class TaskAddComponent {
  statuses: any[] = this.data?.statuses || [];
  @Output() submitForm = new EventEmitter<any>();
  taskForm!: FormGroup;
  

  constructor(private ref: MatDialogRef<TaskAddComponent>, private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any) {
    console.log(data);
    
  }
  ngOnInit() {
    this.taskForm = this.fb.group({
      description: ['', Validators.required],
      dueDate: [null, Validators.required],
      assigneeMail: ['', [Validators.required, Validators.email]],
      currentStatusId: [null, Validators.required]
    });
  }

  onSubmit() {
    if (this.taskForm.valid) {
      const formData = this.taskForm.value;
      this.submitForm.emit(formData)
      this.ref.close();
    }
  }

  modifyDueDate(date:Date){
    console.log(date.getUTCDay)
  }
  CloseModal() {
    this.ref.close();
  }
}
