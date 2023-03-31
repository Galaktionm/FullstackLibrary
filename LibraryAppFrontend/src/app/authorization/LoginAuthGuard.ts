import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Router } from "@angular/router";

@Injectable({
    providedIn: 'root'
})
export class LoginAuthGuard {

    constructor(private router: Router) {}
    
    canActivate(route: ActivatedRouteSnapshot): boolean {
        
        if(sessionStorage.getItem("userId")===null){
            return true;
        }
        this.router.navigate(['/']);
        return false;
    }
    
}