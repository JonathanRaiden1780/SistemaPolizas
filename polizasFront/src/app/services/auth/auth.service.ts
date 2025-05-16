import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, of } from "rxjs";
import { environment } from "../../../environments/environment";
import { SignIn, User, UserRole } from "../../interfaces/Auth";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  private apiUrl = environment.urlBase;
  private currentUser: User | null = null;

  constructor(private http: HttpClient) {
    const userData = sessionStorage.getItem("user");
    if (userData) {
      this.currentUser = JSON.parse(userData);      
    }
  }

  login(credentials: { username: string; password: string }): Observable<SignIn> {
    return this.http.post<SignIn>(`${this.apiUrl}/auth/login`, credentials);
  }

  changePassword(data: { idUser?:number; oldPassword: string; newPassword: string }): Observable<any> {
    data.idUser = this.currentUser?.id;
    return this.http.post(`${this.apiUrl}/auth/change-password`, data);
  }

  logout(): void {
    sessionStorage.removeItem("token");
    sessionStorage.removeItem("user");
    this.currentUser = null;
  }

  getUserRole(): UserRole | null {
    return this.currentUser?.role || null;
  }

  canViewPolicy(): boolean {
    const userRole = this.getUserRole();
    if (!userRole) return false;

    if (userRole === UserRole.ADMIN || userRole === UserRole.BROKER) {
      return true;
    }

    if (userRole === UserRole.CLIENT) {
      return false;
    }

    return false;
  }

  setCurrentUser(user: SignIn): void {
    this.currentUser = user.user;
    sessionStorage.setItem("user", JSON.stringify(user.user));
    sessionStorage.setItem("token", JSON.stringify(user.token));
  }

  getUserName(): string | undefined{
    return this.currentUser?.username;
  }
}
