import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../Services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-task-details',
  templateUrl: './task-details.component.html',
  styleUrls: ['./task-details.component.css']
})
export class TaskDetailsComponent implements OnInit {
  taskId: string;
  grpId: string;
  projectId: string;
  taskDetails: any;
  taskComments: any;
  showAllComments: boolean = false;
  displayedComments: any[] = [];
  allTasksDisplayed:boolean=false;
  commentForm: FormGroup;

  constructor(private route: ActivatedRoute, private authService: AuthService,private formbuilder: FormBuilder) { 
    this.taskId = '';
    this.grpId = '';
    this.projectId = '';

    this.commentForm = this.formbuilder.group({
      commentText: ['', Validators.required]
  });
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.taskId = params['taskId'];
      this.grpId = params['grpId'];
      this.projectId = params['projectId'];
    });
    

    this.getTaskDetails();
    this.getTaskComments();
  }

  getTaskDetails() {
    this.authService.getTaskDetails(this.grpId, this.projectId, this.taskId).subscribe(
      (res) => {
        console.log(res);
        this.taskDetails = res;
        
        this.displayedComments = this.taskComments.slice(0, 3);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getTaskComments() {
    this.authService.getTaskComments(this.grpId, this.projectId, this.taskId).subscribe(
      (res) => {
        this.taskComments = res;
        this.displayedComments=this.taskComments;
        console.log(res);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  showMoreComments(): void {
    this.showAllComments = true;
    this.displayedComments = this.taskComments;
    this.allTasksDisplayed=true;
  }

  addComment() {
    const commentText = this.commentForm.value.commentText;
    console.log(commentText);
    this.authService.postComments(this.grpId, this.projectId, this.taskId,commentText).subscribe((res)=>{
      console.log(res);
      this.getTaskComments();
      this.getTaskDetails();

    },
    (error)=>{
      console.log(error);
    });
    
    this.commentForm.reset();
}

}
