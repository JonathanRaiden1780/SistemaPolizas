import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Policy, PolicyCreate, PolicyStatus } from "../../../interfaces/Policy";
import { PolicyService } from "../../../services/policy/policy.service";
import { ClientsService } from "../../../services/clients/clients.service";
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  standalone: false,
  selector: "app-policy-form-page",
  templateUrl: "./policy-form.page.html",
  styleUrls: ["./policy-form.page.scss"],
})
export class PolicyFormPage implements OnInit {
  policy: Policy | null = null;
  loading = false;
  error = "";
  isEditMode = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private policyService: PolicyService,
    private clientService: ClientsService,
    private modalService: ModalService,
  ) {}

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
      },
      error: (error) => {
        this.error = "Error al cargar la póliza";
        this.modalService.show(this.error, 'error');
        this.loading = false;
      },
      complete:()=> {
        setTimeout(() => {
          this.loading = false
          this.isEditMode = true;
        }, 1000);
      }
    });
  }

  onSubmit(policyData: Policy) {
    this.loading = true;
    const endDate = new Date(policyData.startDate);
    endDate.setMonth(endDate.getMonth() + 1);
    policyData.endDate = endDate;
    this.clientService.createClients(policyData.client).subscribe({
      next: (clientId) => {
        if (clientId) {
          const newPolicy: PolicyCreate = {
            policyNumber: policyData.policyNumber,
            type: policyData.type,
            startDate:  new Date(policyData.startDate),
            endDate: policyData.endDate,
            clientId: clientId,
            amount: policyData.amount,
          };
          const operation = this.isEditMode
            ? this.policyService.updatePolicy(
                this.policy!.policyNumber,
                newPolicy
              )
            : this.policyService.createPolicy(newPolicy);
          operation.subscribe({
            next: (response) => {
              let msj = this.isEditMode
                ? "Póliza actualizada con éxito"
                : "Póliza creada con éxito";
                this.modalService.show(msj, 'success');
              this.router.navigate(["/policies"]);
            },
            error: (error) => {
              this.error = this.isEditMode
                ? "Error al actualizar la póliza"
                : "Error al crear la póliza";
                this.modalService.show(error, 'error');
              
                this.loading = false;
            },
          });
        }
      },
      error: (err) => {
        this.modalService.show(err, 'error');
        console.error(err);
      },
    });
  }
}
