import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { TaskAddComponent } from '../task-add/task-add.component';

@Component({
  selector: 'app-task-view',
  templateUrl: './task-view.component.html',
  styleUrl: './task-view.component.css'
})
export class TaskViewComponent {
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
  count: number | undefined
  taskArrays: Map<string, any[]> = new Map<string, any[]>();


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
    // this.getTaskStatusCount();
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
        this.splitTasksByStatus();
      }
    )
  }

  public getStatuses() {
    this.authService.getStatuses(this.grpId, this.projectId).subscribe((res: any) => {
      this.taskStauses = res;
      console.log(this.taskStauses.length);

      
      // console.log(res)
    },
      (error: any) => {
        console.log(error);
      })
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

  splitTasksByStatus() {
    this.taskArrays = new Map<string, any[]>();
    for (const task of this.tasks) {
      const status = task.currentStatus;
      if (!this.taskArrays.has(status)) {
        this.taskArrays.set(status, [task]);
      } else {
        this.taskArrays.get(status)?.push(task);
      }
    }

    console.log(this.taskArrays);
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

}
