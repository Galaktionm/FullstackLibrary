import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { URLGlobal } from 'src/app/URLGlobal';
import { AuthorApiResult } from '../ApiResult';
import { AddBookRequest, Author } from '../models';

@Component({
  selector: 'app-addbook',
  templateUrl: './addbook.component.html',
  styleUrls: ['./addbook.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AddbookComponent implements OnInit {

  addBookForm!: FormGroup
  currentAuthors!: Author[]
  finalAuthors!: Set<string>;
  resultAuthorIds!: Set<number>;

  constructor(private httpClient:HttpClient) {}


  ngOnInit(): void {
    this.finalAuthors=new Set<string>();
    this.resultAuthorIds=new Set<number>();
    this.addBookForm=new FormGroup({
      title: new FormControl(''),
      description: new FormControl(''),
      authors: new FormControl(''),
      available: new FormControl('')
    });

    this.addBookForm.get("authors")?.valueChanges.subscribe(
      (query)=>{
        var params=new HttpParams().set("query", query);
        this.httpClient.get<AuthorApiResult>(URLGlobal.libraryAppURL+"api/author/search", { params })
        .subscribe({
          next: (value)=>{
            console.log(value);

            document.getElementById("authorsdiv")?.remove();

            var authorsdiv=document.createElement("div");
            authorsdiv.setAttribute("id", "authorsdiv");
            document.getElementById("addbookformdiv")?.append(authorsdiv);

            this.currentAuthors=value.data;
            for(let i=0; i<this.currentAuthors.length; i++){
              var singleresultDiv=document.createElement("button");
              singleresultDiv.setAttribute("data-id", this.currentAuthors[i].authorId.toString());
              singleresultDiv.setAttribute("data-name", this.currentAuthors[i].name);
              singleresultDiv.addEventListener("click", (event)=>{
                this.finalAuthors.add((<HTMLButtonElement>event.target!).dataset["name"]!);
                this.resultAuthorIds.add(Number.parseInt((<HTMLButtonElement>event.target!).dataset["id"]!));
              })
              
              singleresultDiv.append(document.createElement("p").innerHTML=this.currentAuthors[i].name);
              authorsdiv!.append(singleresultDiv);
            }
          },
          error: (error)=>{
            document.getElementById("authorsdiv")?.remove();
          }
      })});
  }

  addBook(){
    var finalData=<number[]>[];
    this.resultAuthorIds.forEach((e)=>{
      finalData.push(e);
    })

    var addBookRequest=<AddBookRequest>{};{
    addBookRequest.title=this.addBookForm.controls["title"].value,
    addBookRequest.description=this.addBookForm.controls["description"].value,
    addBookRequest.authorIds=finalData;
    addBookRequest.available=this.addBookForm.controls["available"].value

    console.log("Sending this: "+addBookRequest);

    this.httpClient.post(URLGlobal.libraryAppURL+"api/book/add", addBookRequest).subscribe({
      next: (result)=>{
        console.log(result);
      }, 
      error: (error)=>{
        console.log(error);
      }
    })

    }
  }
}

