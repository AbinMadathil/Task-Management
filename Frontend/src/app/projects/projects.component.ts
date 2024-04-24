import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectAddComponent } from '../project-add/project-add.component';
import { AuthService } from '../Services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {
  grpid: string=" " ;
  projectDetails: any;
  formData:any;
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalPages:number= 2;

  constructor(private route: ActivatedRoute, private router: Router,private dialog:MatDialog,private authservice:AuthService,private toastr:ToastrService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.grpid = params['grpid'];
      console.log('Group ID:', this.grpid);
    });

    this.getProjectData();
  }

  public getProjectData(){
    this.authservice.getProjects(this.grpid).subscribe((res)=>{
      this.projectDetails=res;
      this.toastr.success("Projects Retrieved Successfully");
    },
    (err)=>{
      console.log(err);
      this.toastr.error("Error while fetching project Data");
    },
    ()=>{
      this.totalPages=this.totalPages = Math.ceil(this.projectDetails.length / this.itemsPerPage); 

    })
  }

  public TaskDetails(Project_Id: string,project_Name:string) {
    console.log(Project_Id);
    this.router.navigate(['/dashboard/projects/tasks'], {
      queryParams: { grpid: this.grpid, projectId: Project_Id, projectName:project_Name }
    });
  }

  AddProjectModal(){
    let dialogref=this.dialog.open(ProjectAddComponent,{
      width:"40%",
      height:"50%"
    },
    );
    
    dialogref.componentInstance.submitForm.subscribe((res: any) => {
      this.formData=res;
      console.log(this.formData)
      if(this.formData){
        this.authservice.submitProject(this.formData,this.grpid).subscribe((response:any)=>{
          // console.log(response);
          this.getProjectData();
        },
        (error)=>{
          console.log(error);
        },
        ()=>{
          this.refreshPage();
        });
      }
    });

  }
  refreshPage() {
  window.location.reload();
  }

  moveNext() {
    this.totalPages = Math.ceil(this.projectDetails.length / this.itemsPerPage);
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  movePrev() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  get paginatedProjects() {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.projectDetails.slice(startIndex, startIndex + this.itemsPerPage);
}
}


