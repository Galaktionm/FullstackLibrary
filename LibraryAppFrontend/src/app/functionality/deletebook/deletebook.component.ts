import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { URLGlobal } from 'src/app/URLGlobal';
import { BookSummary } from '../models';

@Component({
  selector: 'app-deletebook',
  templateUrl: './deletebook.component.html',
  styleUrls: ['./deletebook.component.css']
})
export class DeletebookComponent implements OnInit {

  bookSearchForm!: FormGroup
  searchedBooks!: BookSummary[];
  bookId!: number
  book!: string

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.bookSearchForm=new FormGroup({
      book: new FormControl('')
    });

    this.bookSearchForm.get("book")?.valueChanges.subscribe(
      (query)=>{
        var params=new HttpParams().set("query", query);
        this.httpClient.get<BookSummary[]>(URLGlobal.libraryAppURL+"api/book/search/summary", { params }).subscribe({
          next: (value)=>{
            console.log(value);
            
            document.getElementById("booksdiv")?.remove();

            this.searchedBooks=value;
            var booksDiv=document.createElement("div");
            booksDiv.setAttribute("id", "booksdiv");
            document.getElementById("deleteFormGrid")?.appendChild(booksDiv);
           
            for(let i=0; i<this.searchedBooks.length; i++){
              var singleresultDiv=document.createElement("button");
              singleresultDiv.setAttribute("data-id", this.searchedBooks[i].bookId.toString());
              singleresultDiv.setAttribute("data-title", this.searchedBooks[i].title);
              singleresultDiv.setAttribute("data-author", this.searchedBooks[i].author[0].name);
              singleresultDiv.innerHTML="Title: "+this.searchedBooks[i].title+"<br>"+"Author: "+this.searchedBooks[i].author[0].name;
              booksDiv.appendChild(singleresultDiv);
              singleresultDiv.addEventListener("click", (event)=>{
                  console.log("Clicked!");
                  this.bookId=(Number.parseInt(((<HTMLButtonElement>event.target!).dataset["id"]!)));
                  this.book=((<HTMLButtonElement>event.target!).dataset["title"]!);
                  
                });
          }},
          error: (error)=>{
            document.getElementById("booksdiv")?.remove();
          }

        });
    });
  }

  deleteBook() {
    var params=new HttpParams().set("bookId", this.bookId);

    this.httpClient.delete(URLGlobal.libraryAppURL+"api/book", { params }).subscribe({
      next: (result)=>{
        console.log(result);
      },
      error: (error)=>{
        console.log(error);
      }
    })
  }

}
