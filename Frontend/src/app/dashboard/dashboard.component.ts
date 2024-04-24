import { Component } from '@angular/core';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';
import {MatDialog} from '@angular/material/dialog'
import { GroupAddComponent } from '../group-add/group-add.component';
import { group } from '@angular/animations';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent {
  groups: any;
  groupName!:string;
  constructor(private authService: AuthService, private router:Router,private dialog:MatDialog,private toastr:ToastrService,private spinner:NgxSpinnerService){
    this.spinner.show();
    this.getGroups();
  }

  public getGroups(){
    this.authService.getGroups().subscribe((response)=>
    {
      this.spinner.hide();
      this.groups = response;
      console.log('Groups: ',this.groups);
      this.toastr.success("Groups retrieved Successfully");
    },
    (error) => {
      this.spinner.hide(); // Hide the spinner in case of an error
      console.error('Error fetching groups:', error);
      this.toastr.error("Error Fetching Groups");
    })
  }
  public GetProjectData(grpid: string) : void{
    // this.authService.getProjects(grpid).subscribe(
    //   (response) => {
    //     this.project = response;
    //     console.log(this.project);

        this.router.navigate(['/dashboard/projects'], {
          queryParams: { grpid: grpid },
        });
      
  }

  public OpenAddMethod(){
    let dialogref=this.dialog.open(GroupAddComponent,{
      width:"30%",
      height:"30%"
    });
    
    dialogref.componentInstance.submitForm.subscribe((res: string) => {
      this.groupName=res;
      if(this.groupName){
        const createGroup={
          groupName:this.groupName
        };
        this.authService.submitGroup(createGroup).subscribe((response:any)=>{
          console.log(response);
          this.toastr.success(`Group ${createGroup.groupName} created successfully`);
          this.getGroups();
                  });
      }
    });
    
  }

}