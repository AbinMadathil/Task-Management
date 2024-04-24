import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7197/api'; // Replace with your actual API endpoint

  constructor(private http: HttpClient) {}

  signIn(email: string, password: string): Observable<any> {
    const signInData = { email, password };
    
    return this.http.post(`${this.apiUrl}/login`, signInData ,{responseType:"text"});
  }
  signUpWithoutRefferal(details:any):Observable<any>{
    return this.http.post(`${this.apiUrl}/signup`, details , {responseType:"text"});

  }

  signUpWithRefferal(reqdetails:any):Observable<any>{
    console.log(reqdetails);
    
    return this.http.post(`${this.apiUrl}/signup`, reqdetails,{responseType:"text"});

  }

  submitGroup(groupData:any):Observable<any>{
    return this.http.post(`${this.apiUrl}/groups`,groupData,{responseType:"text"});
  }


  getGroups():Observable<any> {
    return this.http.get(`${this.apiUrl}/groups`);
  }

  getProjects(groupId: string):Observable <any>{
    return this.http.get(`${this.apiUrl}/groups/${groupId}/projects`);
  }
  getTasks(groupId:String,projectId:String):Observable<any>{
    return this.http.get(`${this.apiUrl}/groups/${groupId}/projects/${projectId}/tasks`);
  }
  getStatuses(groupId:String,projectId:String){
    return this.http.get(`${this.apiUrl}/groups/${groupId}/projects/${projectId}/statuses`);
  }

  submitProject(formData:any,grpId:string){
    return this.http.post(`${this.apiUrl}/groups/${grpId}/projects`,formData,{responseType:"text"})
  }

  submitTask(formData:any,grpId:string,projectId:any){
    return this.http.post(`${this.apiUrl}/groups/${grpId}/projects/${projectId}/tasks`,formData,{responseType:"text"})
  }

  getTaskComments(grpId:string,projectId:string,taskId:String){
    return this.http.get(`${this.apiUrl}/groups/${grpId}/projects/${projectId}/tasks/${taskId}/comments`);
  }

  getTaskDetails(grpId:string,projectId:string,taskId:String){
    return this.http.get(`${this.apiUrl}/groups/${grpId}/projects/${projectId}/tasks/${taskId}`);
  }

  postComments(grpId:String,projectId:String,taskId:String,commentText:String){
    const body = { commentText: commentText };
    return this.http.post(`${this.apiUrl}/groups/${grpId}/projects/${projectId}/tasks/${taskId}/comments`,body,{responseType:"text"});
  }
}
