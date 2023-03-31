import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { URLGlobal } from 'src/app/URLGlobal';
import { RegisterRequest } from './registerdto';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registrationForm!:FormGroup

  constructor(private httpClient:HttpClient) {}
  
  
  ngOnInit(): void {

    this.registrationForm=new FormGroup({
      username: new FormControl(''),
      email: new FormControl(''),
      password: new FormControl('')
    });
  }

  register() {
    var registerRequest=<RegisterRequest>{};
    registerRequest.email=this.registrationForm.controls["email"].value
    registerRequest.username=this.registrationForm.controls["username"].value
    registerRequest.password=this.registrationForm.controls["password"].value

    this.httpClient.post(URLGlobal.libraryAppURL+"api/authorization/register", registerRequest).subscribe({
      next: (result)=>{
        console.log(result)
      },

      error: (error)=>{
        console.log(error);
      }
    })
  }

  

}
