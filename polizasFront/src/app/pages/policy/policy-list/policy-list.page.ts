import { Component, OnInit } from "@angular/core";
import { Policy, PolicyStatus } from "../../../interfaces/Policy";
import { PolicyService } from "../../../services/policy/policy.service";
import { AuthService } from "../../../services/auth/auth.service";
import { UserRole } from "../../../interfaces/Auth";
import { ClientsService } from "../../../services/clients/clients.service";
import { ModalService } from "../../../services/modal/modal.service";

@Component({
  standalone: false,
  selector: "app-policy-list",
  templateUrl: "./policy-list.page.html",
  styleUrls: ["./policy-list.page.scss"],
})
export class PolicyListPage implements OnInit {
  policies: Policy[] = [];
  loading = true;
  error = "";
  userRole: UserRole | null;
  filteredPolicies: Policy[] = [];
  filterType: string = "";
  filterClient: string = "";
  constructor(
    private policyService: PolicyService,
    private authService: AuthService,
    private clientService: ClientsService,
    private modalService: ModalService
  ) {
    this.userRole = this.authService.getUserRole();
  }

  ngOnInit() {
    this.loadPolicies();
  }

  loadPolicies() {
    this.loading = true;
    if (!this.authService.canViewPolicy()) {
      let username = this.authService.getUserName();
      this.clientService
        .getClientsByUser(username == undefined ? "" : username)
        .subscribe({
          next: (data) => {
            this.getPolicies(data.id)
          },
          error:(err) => {
              this.modalService.show(err,"error")
          },
        });
    }
    else{
      this.getPolicies()
    }
  }

  getPolicies(clientId?: number) {
    this.policyService.getPolicies(clientId).subscribe({
      next: (policies) => {
        this.policies = policies;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => {
        this.modalService.show(error,"error")
        this.error = "Error loading policies";
        this.loading = false;
      },
    });
  }
  applyFilters() {
    this.filteredPolicies = this.policies.filter((policy) => {
      const matchesType = this.filterType
        ? policy.type === this.filterType
        : true;
      const matchesClient = this.filterClient
        ? policy.client?.name
            ?.toLowerCase()
            .includes(this.filterClient.toLowerCase()) ||
          policy.client?.firstLastName
            ?.toLowerCase()
            .includes(this.filterClient.toLowerCase())
        : true;
      return matchesType && matchesClient;
    });
  }

  onStatusChange(change: boolean) {
    if(change)
      this.loadPolicies()
  }
}
