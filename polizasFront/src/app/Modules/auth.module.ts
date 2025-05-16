import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { LoginPage } from "../pages/auth/login/login.page";
import { RouterModule, Routes } from "@angular/router";
import { MatModule } from "./mat.module";
import { ChangePasswordPage } from "../pages/auth/change-password/change-pasword.page";

const routes: Routes = [
  {
    path: "login",
    component: LoginPage,
  },
  {
    path:"change-password",
    component: ChangePasswordPage
  }
];
@NgModule({
  declarations: [LoginPage,ChangePasswordPage],
  imports: [CommonModule, MatModule, ReactiveFormsModule, RouterModule.forChild(routes)],
})
export class AuthModule {}
