import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../AuthService';
import { LoginRequest } from './logindto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm!:FormGroup
  
  constructor(private authService:AuthService, private router:Router){}
  
  ngOnInit(): void {
     this.loginForm=new FormGroup({
      email: new FormControl('', Validators.email),
      password: new FormControl('', Validators.required)

     })
  }

  onSubmit() {
    var loginRequest=<LoginRequest>{};
    loginRequest.email = this.loginForm.controls['email'].value;
    loginRequest.password = this.loginForm.controls['password'].value;

    this.authService.login(loginRequest).subscribe({
      next: (result)=>{
        console.log(result);
        if(result.success && result.token) {
        this.authService.setUserCredentials(result);
        this.router.navigate(["/"]);
        window.location.reload();
        }
      },

      error: (error)=>{
        console.log(error);
      }
    });
  }

  

}
