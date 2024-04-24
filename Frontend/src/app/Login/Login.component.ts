import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../Services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-Login',
  templateUrl: './Login.component.html',
  styleUrls: ['./Login.component.css']
})
export class LoginComponent implements OnInit {
  authForm! : FormGroup;
  constructor(private fb : FormBuilder,private authService:AuthService ,private router: Router,private toastr: ToastrService) {

   }
  isSignMode : boolean = true;
  ngOnInit(): void {
    this.authForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });    
  }

  onSubmit(): void {
    if (this.authForm.valid) {
      const { email, password } = this.authForm.value;

      if (this.isSignMode) {
        // Sign In logic (you can implement your logic here)
        console.log('Sign In:', email, password);
      } else {
        // Sign Up logic (you can implement your logic here)
        console.log('Sign Up:', email, password);
      }

      this.authService.signIn(email, password).subscribe(
        (response) => {
          
          // Handle successful sign-in, response contains the JWT token
          const jwtToken = response;
          localStorage.setItem("token",JSON.stringify(jwtToken));
          console.log('JWT Token:', jwtToken);
          this.toastr.success("Login Successfull");
          this.router.navigate(['/dashboard']);
        },
        (error) => {
          if (error.error) {
            this.toastr.error( error.error);
          } else {
            this.toastr.error( error);
          }
        }
      );
    }
  }

  toggleMode(): void {
    this.isSignMode = !this.isSignMode;
    this.authForm.reset();
  }

}
