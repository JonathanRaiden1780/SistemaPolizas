import { NgModule } from "@angular/core";
import { PreloadAllModules, RouterModule, Routes } from "@angular/router";
import { AuthGuard } from "./guards/auth.guards";

const routes: Routes = [
  {
    path: "auth",
    loadChildren: () => import("./Modules/auth.module").then((m) => m.AuthModule),
  },
  {
    path: "policies",
    canActivate: [AuthGuard],
    loadChildren: () => import("./Modules/policy.module").then((m) => m.PolicyModule),
  },
  {
    path: "",
    redirectTo: "auth/login",
    pathMatch: "full",
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
