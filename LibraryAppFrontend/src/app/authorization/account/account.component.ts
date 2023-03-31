import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/functionality/models';
import { URLGlobal } from 'src/app/URLGlobal';
import { AuthService } from '../AuthService';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  user!:User
  isAdmin!: boolean
  
  constructor(private httpClient: HttpClient, private authService: AuthService) {}

  ngOnInit(): void {
    var params=new HttpParams().set("userId", sessionStorage.getItem("userId")!);
    this.httpClient.get<User>(URLGlobal.libraryAppURL+"api/authorization/account", { params }).subscribe({
      next: (result)=>{
        console.log(result);
        this.user=result;
        this.isAdmin=this.authService.isAdmin();
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

}
