import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../../../services/auth/auth.service";
import { UserRole } from "../../../interfaces/Auth";

@Component({
  standalone: false,
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrls: ["./header.component.scss"],
})
export class HeaderComponent {
  userRole: UserRole | null;
  mobileMenuOpen = false;
  userName?:string = ''; 

  constructor(private authService: AuthService, private router: Router) {
    this.userRole = this.authService.getUserRole();
    this.userName = authService.getUserName();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/auth/login"]);
  }

  canCreatePolicy(): boolean {
    return this.userRole === UserRole.ADMIN || this.userRole === UserRole.BROKER || this.userRole === UserRole.CLIENT;
  }

  toggleMobileMenu() {
    this.mobileMenuOpen = !this.mobileMenuOpen;
  }
  
  closeMobileMenu() {
    this.mobileMenuOpen = false;
  }
}
