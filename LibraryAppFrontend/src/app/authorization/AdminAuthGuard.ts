import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "./AuthService";

@Injectable({
    providedIn: 'root'
})
export class AdminAuthGuard {

    constructor(private router: Router) {}
    
    canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        
        if(sessionStorage.getItem("isAdmin")==="true"){
            return true;
        }
        this.router.navigate(['/']);
        return false;
    }
    
}