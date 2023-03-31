import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookdashboardComponent } from './bookdashboard/bookdashboard.component';
import { BookdisplayComponent } from './bookdisplay/bookdisplay.component';
import { AppRoutingModule } from '../app-routing.module';
import { BookdetailsComponent } from './bookdetails/bookdetails.component';
import { EditBookComponent } from './editbook/editbook.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddbookComponent } from './addbook/addbook.component';
import { AddCheckoutComponent } from './add-checkout/add-checkout.component';
import { AdminpanelComponent } from './adminpanel/adminpanel.component';
import { DeletebookComponent } from './deletebook/deletebook.component';



@NgModule({
  declarations: [
    BookdashboardComponent,
    BookdisplayComponent,
    BookdetailsComponent,
    EditBookComponent,
    AddbookComponent,
    AddCheckoutComponent,
    AdminpanelComponent,
    DeletebookComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    ReactiveFormsModule
  ],
  exports: [
    BookdashboardComponent,
    BookdisplayComponent,
    BookdetailsComponent,
    EditBookComponent,
    AddbookComponent
  ]
})
export class FunctionalityModule { }
