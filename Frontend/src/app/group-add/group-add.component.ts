import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-group-add',
  templateUrl: './group-add.component.html',
  styleUrl: './group-add.component.css'
})
export class GroupAddComponent {
  @Output() submitForm = new EventEmitter<string>();
  groupNameForm!: FormGroup;
  constructor(private ref: MatDialogRef<GroupAddComponent>, private fb: FormBuilder) { }
  ngOnInit() {
    this.groupNameForm = this.fb.group({
      groupName: ['', [Validators.required, Validators.maxLength(50)]],
    });
  }

  onSubmit() {
    if (this.groupNameForm.valid) {
      const groupName = this.groupNameForm.value.groupName;
      console.log(groupName);
      
      this.submitForm.emit(groupName);
      this.ref.close();
    }
  }

  CloseModal() {
    this.ref.close();
  }
}
