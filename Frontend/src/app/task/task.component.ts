import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { HttpResponse } from '@angular/common/http';
import { MatDialog } from '@angular/material/dialog';
import { TaskAddComponent } from '../task-add/task-add.component';
import { NgxSpinnerService } from 'ngx-spinner';



@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrl: './task.component.css'
})
export class TaskComponent implements OnInit {
  grpId: string = "";
  projectId: string = "";
  projectName: String = "";
  tasks: any;
  taskStauses: any;
  formData: any;
  loaded: boolean = false;
  modalClosed: boolean = false;
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages: number | undefined;

  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService, private dialog: MatDialog, private spinner: NgxSpinnerService) {
    this.route.queryParams.subscribe(params => {
      this.grpId = params['grpid'];
      this.projectId = params['projectId'];
      this.projectName = params['projectName'];

      console.log('Group ID:', this.grpId);
      console.log('Project ID:', this.projectId);
      console.log('Project Details:', this.projectName);
    });
  }
  ngOnInit() {
    this.spinner.show();
    this.getTasks();
    this.getStatuses();

  }

  public getTasks() {
    this.authService.getTasks(this.grpId, this.projectId).subscribe((res: any) => {
      console.log(res);
      this.tasks = res
      this.spinner.hide();
      if (this.modalClosed) {
        this.loaded = true;
      }
    },
      (error: any) => {
        this.spinner.hide();
        console.log(error)
      },
      () => {
        this.loaded = true;
        this.totalPages = this.totalPages = Math.ceil(this.tasks.length / this.itemsPerPage);
      }
    )
  }

  public getStatuses() {
    this.authService.getStatuses(this.grpId, this.projectId).subscribe((res: any) => {
      this.taskStauses = res;
      console.log(res)
    },
      (error: any) => {
        console.log(error);
      })
  }

  AddTasksModal() {
    let dialogref = this.dialog.open(TaskAddComponent, {
      width: "40%",
      height: "70%",
      data: {
        statuses: this.taskStauses
      }
    }
    );

    dialogref.componentInstance.submitForm.subscribe((res: any) => {
      this.formData = res;
      console.log(this.formData)
      if (this.formData) {
        this.authService.submitTask(this.formData, this.grpId, this.projectId).subscribe((response: any) => {
          this.getTasks();
        },
          (error: any) => {
            console.log(error);
          },
          () => {
            this.totalPages = this.totalPages = Math.ceil(this.tasks.length / this.itemsPerPage);
          }
        );
      }
    });

    dialogref.afterClosed().subscribe(() => {
      this.modalClosed = true; // Set modalClosed to true when modal is closed
      this.loaded = false; // Show loading template when modal is closed
    });

  }

  getStatusColor(text: string): string | null {
    if (Array.isArray(this.taskStauses) && this.taskStauses.length > 0) {
      for (const status of this.taskStauses) {
        if (status && status.statusText && status.statusColor) {
          if (status.statusText === text) {
            return status.statusColor;
          }
        }
      }
    }
    return null;
  }

  moveNext() {
    this.totalPages = Math.ceil(this.tasks.length / this.itemsPerPage);
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  movePrev() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  get paginatedTasks() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.tasks.slice(startIndex, startIndex + this.itemsPerPage);
  }

}
