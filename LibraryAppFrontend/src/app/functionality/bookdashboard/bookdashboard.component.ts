import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { URLGlobal } from 'src/app/URLGlobal';
import { BookApiResult } from '../ApiResult';
import { BookSummary } from '../models';

@Component({
  selector: 'app-bookdashboard',
  templateUrl: './bookdashboard.component.html',
  styleUrls: ['./bookdashboard.component.css']
})
export class BookdashboardComponent implements OnInit {
  
  result!: BookApiResult;
  pageSize!: number;
  pageIndex!: number;
  isAdmin!: boolean;
  hostURL!: string;

  constructor(private httpClient: HttpClient) {}
  
  ngOnInit(): void {
    this.pageSize=6;
    this.pageIndex=0;
    this.isAdmin=(sessionStorage.getItem("isAdmin")==="true");
    this.hostURL=URLGlobal.libraryAppURL+"api/operations/image/?imagePath=";
    this.getData();
    console.log("For debugging: "+this.isAdmin);
  }

  getData() {
    var params=new HttpParams().set("pageIndex", this.pageIndex).set("pageSize", this.pageSize)
    this.httpClient.get<BookApiResult>(URLGlobal.libraryAppURL+"api/book/all", { params }).subscribe({
        next: (result)=>{
          console.log(result);
          this.result=result;
        },
        error: (error)=>{
          console.log(error);
        }
    })};

    moveToNextPage() {
      if(this.pageIndex+1<this.result.totalPages){
        this.pageIndex+=1;
        this.getData();
      }
    }

    moveToPreviousPage() {
      if(this.pageIndex>0){
         this.pageIndex-=1;
         this.getData();
      }
    }

}
