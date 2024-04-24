import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppComponent } from './app.component';
import { BrowserModule } from '@angular/platform-browser';
import { LoginComponent } from './Login/Login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from './Services/auth.service';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule, RouterOutlet } from '@angular/router';
import { routes } from './app.routes';
import { appInterceptor } from './app.interceptor';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProjectsComponent } from './projects/projects.component';
import { SignupComponent } from './signup/signup.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';import {trigger,state,style,animate,transition}from '@angular/animations';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { TaskComponent } from './task/task.component';
import {MatDialogModule} from '@angular/material/dialog'
import { GroupAddComponent } from './group-add/group-add.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ProjectAddComponent } from './project-add/project-add.component';
import { TaskAddComponent } from './task-add/task-add.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TaskDetailsComponent } from './task-details/task-details.component';
import { TaskViewComponent } from './task-view/task-view.component';

@NgModule({
  declarations: [AppComponent ,LoginComponent,SignupComponent , DashboardComponent,ProjectsComponent,TaskComponent,GroupAddComponent,ProjectAddComponent,TaskAddComponent,PageNotFoundComponent,
                  TaskDetailsComponent,TaskViewComponent],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterOutlet,
    RouterModule.forRoot(routes),
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),
    MatDialogModule,
    NgxSpinnerModule.forRoot({ type: 'ball-scale-multiple' }),
    MatDatepickerModule,
    MatInputModule,
    MatNativeDateModule,
  ],
  bootstrap:[AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [AuthService , {provide: HTTP_INTERCEPTORS ,useClass: appInterceptor , multi:true}, provideAnimationsAsync()]
  
})
export class AppModule { }
