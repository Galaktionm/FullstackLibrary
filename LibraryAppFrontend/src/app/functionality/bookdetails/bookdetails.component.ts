import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { AuthService } from 'src/app/authorization/AuthService';
import { URLGlobal } from 'src/app/URLGlobal';
import { AddReviewRequest, BookDetails } from '../models';

@Component({
  selector: 'app-bookdetails',
  templateUrl: './bookdetails.component.html',
  styleUrls: ['./bookdetails.component.css']
})
export class BookdetailsComponent implements OnInit {
  
  book!: BookDetails
  bookId!: number
  isAdmin!: boolean
  imagePath!: string

  addReviewForm!: FormGroup

  constructor(
    private httpClient:HttpClient, 
    private authService:AuthService,
    private activatedRoute: ActivatedRoute) {}

  ngOnInit(): void {

    this.addReviewForm=new FormGroup({
      review: new FormControl(),
      rating: new FormControl()
    });


    this.activatedRoute.params.subscribe((params: Params)=> {
      this.bookId=params["bookId"];
      this.httpClient.get<BookDetails>(URLGlobal.libraryAppURL+"api/book/"+params["bookId"]).subscribe({
        next: (result)=>{
          console.log(result);
          if(result.imagePath!=null){
          this.imagePath=URLGlobal.libraryAppURL+"api/operations/image/?imagePath="+result.imagePath;
          }
          this.book=result;
        },
        error: (error)=>{
          console.log(error);
        }
      })});
  }

  addImage(event: Event){

    var fileInputElement=<HTMLInputElement>event.target!;
    var image=fileInputElement.files![0];

    const formData=new FormData();
    formData.append("image", image);
    
    //var extension=image.name.split('.').pop();
    
    var params=new HttpParams().append("bookId", this.bookId);

    this.httpClient.post(URLGlobal.libraryAppURL+"api/operations/image/upload", formData, { params }).subscribe({
        next: (result)=>{
          window.location.reload();
          console.log(result)
        },
        error: (error)=>{
          console.log(error)
        }
    })

  }

  addReview() {

    var addReviewRequest=<AddReviewRequest>{};
    addReviewRequest.userId=sessionStorage.getItem("userId")!;
    addReviewRequest.bookId=this.bookId;
    addReviewRequest.rating=this.addReviewForm.controls["rating"].value;
    addReviewRequest.review=this.addReviewForm.controls["review"].value;


    this.httpClient.post(URLGlobal.libraryAppURL+"api/operations/review/add", addReviewRequest).subscribe({
      next: (result)=>{
        window.location.reload();
        console.log(result);
      }, 

      error: (error)=>{
        console.log(error);
      }
    })


  }

  

}
