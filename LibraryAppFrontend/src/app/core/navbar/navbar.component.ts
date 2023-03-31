import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AuthService } from 'src/app/authorization/AuthService';
import { BookApiResult } from 'src/app/functionality/ApiResult';
import { URLGlobal } from 'src/app/URLGlobal';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  
  searchForm!: FormGroup
  searchResult!: BookApiResult
  isAuthenticated!: boolean

  constructor(private httpClient: HttpClient, private authService:AuthService) {}
  
  ngOnInit(): void {

    this.isAuthenticated=this.authService.isAuthenticated();

    this.searchForm=new FormGroup({
      title: new FormControl('')
    });
    this.searchForm.get("title")?.valueChanges.subscribe(
      (query)=>{
        var params=new HttpParams().set("query", query);
        this.httpClient.get<BookApiResult>(URLGlobal.libraryAppURL+"api/book/search", { params })
        .subscribe({
          next: (value)=>{
            this.searchResult=value;
            console.log(value);
          }
        })});
  }

  
}
