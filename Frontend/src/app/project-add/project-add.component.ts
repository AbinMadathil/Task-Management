import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-project-add',
  templateUrl: './project-add.component.html',
  styleUrl: './project-add.component.css'
})
export class ProjectAddComponent {

  @Output() submitForm = new EventEmitter<any>();
  projectCreateForm!: FormGroup;
  constructor(private ref: MatDialogRef<ProjectAddComponent>, private fb: FormBuilder, @Inject(MAT_DIALOG_DATA) public data: any,private toastr:ToastrService) {

  }
  ngOnInit() {
    this.projectCreateForm = this.fb.group({
      projectName: ['', Validators.required],
      projectDescription: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.projectCreateForm.valid) {
      const formData = this.projectCreateForm.value  ;
      this.submitForm.emit(formData);
      // this.toastr.success(`Added Project ${formData.projectName} Successfully`);
      this.ref.close();
    }
  }

  CloseModal() {
    this.ref.close();
  }
}
