import { Routes } from '@angular/router';
import { LoginComponent } from './Login/Login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectsComponent } from './projects/projects.component';
import { SignupComponent } from './signup/signup.component';
import { TaskComponent } from './task/task.component';
import { AuthGuard } from './auth.guard';
import { DashGuard } from './dash.guard';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TaskAddComponent } from './task-add/task-add.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { TaskViewComponent } from './task-view/task-view.component';

export const routes: Routes = [
    {path:'',component:LoginComponent,canActivate:[AuthGuard]},
    { path: 'dashboard', canActivate: [DashGuard], children: [
        { path: '', component: DashboardComponent },
        { path: 'projects', component: ProjectsComponent },
        { path: 'projects/tasks', component: TaskViewComponent },
        { path: 'projects/tasks/comments', component: TaskDetailsComponent },
    ]},
    {path:'signup',component:SignupComponent},
    {path:'**',component:PageNotFoundComponent}
    // {path:'dashboard',component:DashboardComponent,canActivate:[DashGuard]},
    // {path:'dashboard/projects',component:ProjectsComponent},
    
    // {path:'dashboard/projects/tasks',component:TaskComponent}
    
    // {path:'signup',component:}
];
