import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  SignForm: FormGroup;

  constructor(private fb: FormBuilder,private authservice:AuthService ,private router:Router,private toastr:ToastrService) {
    this.SignForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      name: ['', [Validators.required]],
      groupName: ['', [Validators.required]],
      referralCode: [''] // Optional field, adjust as needed
    });
  }
  ngOnInit(): void {
  }

  onSubmit(): void {
    if (this.SignForm.valid) {
      const formData = this.SignForm.value;
      if (formData.referralCode) {
        const requestDataWithReferral = {
          email: formData.email,
          password: formData.password,
          name: formData.name,
          groupName: formData.groupName,
          referralCode: formData.referralCode
        };
        console.log(requestDataWithReferral);
        
        var reqdata=JSON.stringify(requestDataWithReferral)
        this.authservice.signUpWithRefferal(requestDataWithReferral).subscribe((response:any)=>{
          const jwtToken = response;
          localStorage.setItem("token",JSON.stringify(jwtToken));
          console.log('JWT Token:', jwtToken);
          this.toastr.success("SignUp Successfull");
          this.router.navigate(['/dashboard']);
        },
        (error) => {
          if (error.error) {
            this.toastr.error( error.error);
          } else {
            this.toastr.error( error);
          }
        });
  
      } else {
        const requestDataWithoutReferral = {
          email: formData.email,
          password: formData.password,
          name: formData.name,
          groupName: formData.groupName
        };
        var requesteddata=JSON.stringify(requestDataWithoutReferral)
        this.authservice.signUpWithoutRefferal(requestDataWithoutReferral).subscribe((response:any)=>{
          const jwtToken = response;
          localStorage.setItem("token",JSON.stringify(jwtToken));
          console.log('JWT Token:', jwtToken);
          this.toastr.success("SignUp successfull");
          this.router.navigate(['/dashboard']);
      },
      (error) => {
        if (error.error) {
          this.toastr.error( error.error);
        } else {
          this.toastr.error( error);
        }
      });
    }
  }

  }
  toggleMode(): void {

    // Reset the form to its initial state
    this.SignForm.reset({
      email: '',
      password: '',
      name: '',
      groupName: '',
      referralCode: ''
    });
  }

}

