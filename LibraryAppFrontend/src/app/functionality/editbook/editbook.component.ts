import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { AuthService } from 'src/app/authorization/AuthService';
import { URLGlobal } from 'src/app/URLGlobal';
import { BookDetails } from '../models';

@Component({
  selector: 'app-editbook',
  templateUrl: './editbook.component.html',
  styleUrls: ['./editbook.component.css']
})
export class EditBookComponent implements OnInit {
  
  book!: BookDetails
  editForm!: FormGroup

  constructor(
    private httpClient:HttpClient, 
    private authService:AuthService,
    private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {
      this.editForm=new FormGroup({
      title: new FormControl(''),
      description: new FormControl(''),
      available: new FormControl('')
    });

    this.activatedRoute.params.subscribe((params: Params)=> {
      this.httpClient.get<BookDetails>(URLGlobal.libraryAppURL+"api/book/"+params["bookId"]).subscribe({
        next: (result)=>{
          console.log(result);
          this.book=result;
        },
        error: (error)=>{
          console.log(error);
        }
      })});
  }

  sendUpdateRequest(){
    var request={
      bookid: this.book.bookId,
      title: this.editForm.controls['title'].value,
      description: this.editForm.controls['description'].value,
      available: this.editForm.controls['available'].value
    }

    this.httpClient.post(URLGlobal.libraryAppURL+'api/book/update', request).subscribe({
      next: (result)=>{
        console.log(result);
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

  

}