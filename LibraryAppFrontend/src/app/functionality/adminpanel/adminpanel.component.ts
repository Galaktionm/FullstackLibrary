import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { URLGlobal } from 'src/app/URLGlobal';
import { User } from '../models';

@Component({
  selector: 'app-adminpanel',
  templateUrl: './adminpanel.component.html',
  styleUrls: ['./adminpanel.component.css']
})
export class AdminpanelComponent implements OnInit {
  
  user!: User

  constructor(private httpClient:HttpClient) {}
  
  ngOnInit(): void {
    var params=new HttpParams().set("userId", sessionStorage.getItem("userId")!);
    this.httpClient.get<User>(URLGlobal.libraryAppURL+"api/authorization/account", { params }).subscribe({
      next: (result)=>{
        console.log(result);
        this.user=result;
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

  

}
