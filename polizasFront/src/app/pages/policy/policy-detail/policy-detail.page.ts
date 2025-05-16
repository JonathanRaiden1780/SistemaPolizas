import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Policy, PolicyStatus } from "../../../interfaces/Policy";
import { PolicyService } from "../../../services/policy/policy.service";
import { AuthService } from "../../../services/auth/auth.service";
import { UserRole } from "../../../interfaces/Auth";

@Component({
  standalone: false,
  selector: "app-policy-detail",
  templateUrl: "./policy-detail.page.html",
  styleUrls: ["./policy-detail.page.scss"],
})
export class PolicyDetailPage implements OnInit {
  policy: Policy | null = null;
  loading = true;
  error = "";
  userRole: UserRole | null;

  constructor(private route: ActivatedRoute, private router: Router, private policyService: PolicyService, private authService: AuthService) {
    this.userRole = this.authService.getUserRole();
  }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get("id");
    if (id) {
      this.loadPolicy(id);
    }
  }

  loadPolicy(id: string) {
    this.loading = true;
    this.policyService.getPolicyById(id).subscribe({
      next: (policy) => {
        this.policy = policy;
        this.loading = false;
      },
      error: (error) => {
        this.error = "Error al cargar la póliza";
        this.loading = false;
      },
    });
  }

  canEdit(): boolean {
    if (!this.policy) return false;
    return (this.userRole === UserRole.ADMIN ) && this.policy.status === PolicyStatus.Cotizada;
  }

  onEdit() {
    if (this.policy) {
      this.router.navigate(["/policies/edit", this.policy.policyNumber]);
    }
  }
}
