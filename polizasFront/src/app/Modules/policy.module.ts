import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { PolicyListPage } from "../pages/policy/policy-list/policy-list.page";
import { PolicyFormPage } from "../pages/policy/policy-form/policy-form.page";
import { PolicyDetailPage } from "../pages/policy/policy-detail/policy-detail.page";
import { PolicyFormComponent } from "../components/policy/policy-form/policy-form.component";
import { PolicycardComponent } from "../components/policy/policycard/policycard.component";
import { RoleGuard } from "../guards/role.guard";
import { MatModule } from "./mat.module";

const routes: Routes = [
  {
    path: "",
    component: PolicyListPage,
  },
  {
    path: "new",
    component: PolicyFormPage,
  },
  {
    path: "edit/:id",
    component: PolicyFormPage,
    canActivate: [RoleGuard],
  },
  {
    path: "detail/:id",
    component: PolicyDetailPage,
  },
];

@NgModule({
  declarations: [PolicyListPage, PolicyFormPage, PolicyDetailPage, PolicyFormComponent, PolicycardComponent],
  imports: [CommonModule, FormsModule ,MatModule,ReactiveFormsModule, RouterModule.forChild(routes)],
})
export class PolicyModule {}
