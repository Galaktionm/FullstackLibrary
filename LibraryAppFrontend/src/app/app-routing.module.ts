import { inject, NgModule } from '@angular/core';
import { ActivatedRouteSnapshot, RouterModule, Routes } from '@angular/router';
import { AccountComponent } from './authorization/account/account.component';
import { AdminAuthGuard } from './authorization/AdminAuthGuard';
import { LoginComponent } from './authorization/login/login.component';
import { LoginAuthGuard } from './authorization/LoginAuthGuard';
import { RegisterComponent } from './authorization/register/register.component';
import { AddCheckoutComponent } from './functionality/add-checkout/add-checkout.component';
import { AddbookComponent } from './functionality/addbook/addbook.component';
import { AdminpanelComponent } from './functionality/adminpanel/adminpanel.component';
import { BookdashboardComponent } from './functionality/bookdashboard/bookdashboard.component';
import { BookdetailsComponent } from './functionality/bookdetails/bookdetails.component';
import { DeletebookComponent } from './functionality/deletebook/deletebook.component';
import { EditBookComponent } from './functionality/editbook/editbook.component';

const routes: Routes = [
  { path: "login", component: LoginComponent, canActivate: [(snaphot: ActivatedRouteSnapshot)=>inject(LoginAuthGuard).canActivate(snaphot)]},
  { path: "register", component: RegisterComponent, canActivate: [(snaphot: ActivatedRouteSnapshot)=>inject(LoginAuthGuard).canActivate(snaphot)]},
  { path: "account", component: AccountComponent },
  { path: "adminpanel", component: AdminpanelComponent, canActivate: [(snapshot: ActivatedRouteSnapshot)=>inject(AdminAuthGuard).canActivate(snapshot)]},
  { path: "books", component: BookdashboardComponent },
  { path: "book/add", component: AddbookComponent, canActivate: [(snapshot: ActivatedRouteSnapshot)=>inject(AdminAuthGuard).canActivate(snapshot)]}, 
  { path: "book/delete", component: DeletebookComponent, canActivate: [(snapshot: ActivatedRouteSnapshot)=>inject(AdminAuthGuard).canActivate(snapshot)]},
  { path: "book/:bookId", component: BookdetailsComponent },
  { path: "book/edit/:bookId", component: EditBookComponent, canActivate: [(snapshot: ActivatedRouteSnapshot)=>inject(AdminAuthGuard).canActivate(snapshot)]},
  { path: "checkout/add", component: AddCheckoutComponent, canActivate: [(snapshot: ActivatedRouteSnapshot)=>inject(AdminAuthGuard).canActivate(snapshot)]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
