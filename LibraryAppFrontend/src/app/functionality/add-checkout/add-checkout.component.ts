import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { URLGlobal } from 'src/app/URLGlobal';
import { AddCheckoutRequest, BookReview, BookSummary, Checkout, User } from '../models';

@Component({
  selector: 'app-add-checkout',
  templateUrl: './add-checkout.component.html',
  styleUrls: ['./add-checkout.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AddCheckoutComponent implements OnInit {

  searchedUsers!: User[];
  user!: User
  userId!: string;


  searchedBooks!: BookSummary[];
  bookPageIndex!: number
  books!: Set<string>;
  bookIds!: Set<number>

  checkoutForm!: FormGroup

  constructor(private httpClient: HttpClient) {}
  
  ngOnInit(): void {
    this.bookIds=new Set<number>();
    this.books=new Set<string>();
    this.checkoutForm=new FormGroup({
      user: new FormControl(''),
      book: new FormControl(''),
      returnDate: new FormControl('')
    });


    this.checkoutForm.get("user")?.valueChanges.subscribe(
      (query)=>{
        var params=new HttpParams().set("email", query);
        this.httpClient.get<User[]>(URLGlobal.libraryAppURL+"api/authorization", { params }).subscribe({
          next: (value)=>{
            console.log(value);

            console.log(value);
            
            document.getElementById("usersdiv")?.remove();
            document.getElementById("booksdiv")?.remove();

            this.searchedUsers=value;
            var usersDiv=document.createElement("div");
            usersDiv.setAttribute("id", "usersdiv");
            document.getElementById("checkoutformgrid")?.appendChild(usersDiv);
           
            for(let i=0; i<this.searchedUsers.length; i++){
              var singleresultDiv=document.createElement("button");
              singleresultDiv.setAttribute("data-id", this.searchedUsers[i].userId.toString());
              singleresultDiv.setAttribute("data-email", this.searchedUsers[i].email);
              singleresultDiv.innerHTML=this.searchedUsers[i].email;
              usersDiv.appendChild(singleresultDiv);
              singleresultDiv.addEventListener("click", (event)=>{
                  this.user=this.searchedUsers[i];
                  this.userId=((<HTMLButtonElement>event.target!).dataset["id"]!);
              });
          }},
          error: (error)=>{
            document.getElementById("usersdiv")?.remove();
            document.getElementById("booksdiv")?.remove();
          }

        });
    });


    this.checkoutForm.get("book")?.valueChanges.subscribe(
      (query)=>{
        var params=new HttpParams().set("query", query);
        this.httpClient.get<BookSummary[]>(URLGlobal.libraryAppURL+"api/book/search/summary", { params }).subscribe({
          next: (value)=>{
            console.log(value);
            
            document.getElementById("usersdiv")?.remove();
            document.getElementById("booksdiv")?.remove();

            this.searchedBooks=value;
            var booksDiv=document.createElement("div");
            booksDiv.setAttribute("id", "booksdiv");
            document.getElementById("checkoutformgrid")?.appendChild(booksDiv);
           
            for(let i=0; i<this.searchedBooks.length; i++){
              var singleresultDiv=document.createElement("button");
              singleresultDiv.setAttribute("data-id", this.searchedBooks[i].bookId.toString());
              singleresultDiv.setAttribute("data-title", this.searchedBooks[i].title);
              singleresultDiv.setAttribute("data-author", this.searchedBooks[i].author[0].name);
              singleresultDiv.innerHTML="Title: "+this.searchedBooks[i].title+"<br>"+"Author: "+this.searchedBooks[i].author[0].name;
              booksDiv.appendChild(singleresultDiv);
              singleresultDiv.addEventListener("click", (event)=>{
                  console.log("Clicked!");
                  this.bookIds.add(Number.parseInt(((<HTMLButtonElement>event.target!).dataset["id"]!)));
                  this.books.add((<HTMLButtonElement>event.target!).dataset["title"]!);
                  
                });
          }},
          error: (error)=>{
            document.getElementById("usersdiv")?.remove();
            document.getElementById("booksdiv")?.remove();
          }

        });
    });
}

sendCheckoutRequest(){
  var request=<AddCheckoutRequest>{};
  var bookIdsArray=<number[]>[];
  this.bookIds.forEach(e=>{
    bookIdsArray.push(e);
  })
  request.bookIds=bookIdsArray;
  request.until=this.checkoutForm.controls["returnDate"].value;
  request.userId=this.user.userId;

  this.httpClient.post(URLGlobal.libraryAppURL+"api/checkout", request).subscribe({
    next: (value)=>{
      console.log(value);
    }, 
    error: (error)=>{
      console.log(error);
    }
  })
  
}

}
