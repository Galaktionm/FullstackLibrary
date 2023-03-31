import { Component, Input } from '@angular/core';
import { URLGlobal } from 'src/app/URLGlobal';
import { BookAuthor } from '../models';

@Component({
  selector: 'app-bookdisplay',
  templateUrl: './bookdisplay.component.html',
  styleUrls: ['./bookdisplay.component.css']
})
export class BookdisplayComponent {
  
  @Input() bookId!: number;
  @Input() title! : String;
  @Input() description: String='';
  @Input() authors!: BookAuthor[];
  @Input() isAdmin!: boolean;
  @Input() imagePath!: string;

  hostPath: string=URLGlobal.libraryAppURL+"api/operations/image/?imagePath="


}
