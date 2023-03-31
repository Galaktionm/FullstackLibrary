
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResult } from './login/logindto';
import { URLGlobal } from '../URLGlobal';
import { RegisterRequest } from './register/registerdto';


@Injectable({
 providedIn: 'root',
})
export class AuthService {
 constructor(protected http: HttpClient) {}

 getToken():string|null{
    var token=sessionStorage.getItem("token");
   return token; 
 }

 login(item: LoginRequest): Observable<LoginResult> {
    var url = URLGlobal.libraryAppURL + "api/authorization/login";
    return this.http.post<LoginResult>(url, item);
}

setUserCredentials(result: LoginResult) {
    sessionStorage.setItem("token", result.token);
    sessionStorage.setItem("userId", result.userId);
    sessionStorage.setItem("isAdmin", result.isAdmin.toString());
}

logout() {
    sessionStorage.removeItem("token");
}

register(item: RegisterRequest):Observable<any>{
    var url = URLGlobal.libraryAppURL + "api/authorization/register";
    return this.http.post(url, item);
}

isAuthenticated():boolean{
    if(sessionStorage.getItem("userId")!=null){
        return true;
    }
    return false;
}

isAdmin() : boolean {
    if(sessionStorage.getItem("isAdmin")==="true") {
        return true;
    } 
    return false;
}

}