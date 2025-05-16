import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Policy, type, PolicyStatus } from "../../../interfaces/Policy";
import { v4 as uuidv4 } from "uuid";
import { AuthService } from "../../../services/auth/auth.service";
import { ClientsService } from "../../../services/clients/clients.service";
import { ModalService } from "../../../services/modal/modal.service";

@Component({
  standalone: false,
  selector: "app-policy-form",
  templateUrl: "./policy-form.component.html",
  styleUrls: ["./policy-form.component.scss"],
})
export class PolicyFormComponent implements OnInit {
  @Input() policy: Policy | null = null;
  @Output() submitForm = new EventEmitter<Policy>();

  policyForm: FormGroup;
  types = Object.values(type);
  disabled: boolean = false;
  todayString = new Date().toISOString().substring(0, 10);

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private modalService: ModalService,
    private clientService: ClientsService
  ) {
    const guid = uuidv4().toUpperCase();

    this.policyForm = this.fb.group({
      policyNumber: [guid, Validators.required],
      type: ["", Validators.required],
      client: this.fb.group({
        name: ["", Validators.required],
        secondLastName: ["", Validators.required],
        firstLastName: ["", Validators.required],
        age: [
          "",
          [Validators.required, Validators.min(18), Validators.max(99)],
        ],
        birthCountry: ["", Validators.required],
        gender: ["", Validators.required],
        email: ["", [Validators.required, Validators.email]],
        phone: [
          "",
          [
            Validators.required,
            Validators.minLength(10),
            Validators.maxLength(10),
            Validators.pattern("^[0-9]*$"),
          ],
        ],
      }),
      startDate: [this.todayString, Validators.required],
      endDate: [""],
      amount: [{ value: 0, disabled: true }, Validators.required],
    });
    this.policyForm.get("policyNumber")?.disable();
    const today = new Date().toISOString().substring(0, 10);
    this.policyForm.get("startDate")?.setValue(today);
  }

  ngOnInit() {
    if (this.policy) {
      const formattedPolicy = {
        ...this.policy,
        startDate: this.policy.startDate.toString()?.substring(0, 10) || "",
        endDate: this.policy.endDate.toString()?.substring(0, 10) || "",
      };
      this.policyForm.patchValue(formattedPolicy);
    } else if (!this.authService.canViewPolicy()) {
      this.disabled = true;
      let username = this.authService.getUserName();
      this.clientService.getClientsByUser(username ?? "").subscribe({
        next: (data) => {
          this.policyForm.get("client")?.patchValue(data);
          this.policyForm.get("client")?.disable();
        },
        error: (err) => {
          this.modalService.show(err, "error");
        },
      });
    }
  }

  onSubmit(): void {
    if (this.policyForm.valid) {
      const formValue = {
        ...this.policyForm.getRawValue(),
        amount: this.policyForm.get("amount")?.value,
      };

      this.submitForm.emit(formValue);
    } else {
      this.markFormGroupTouched(this.policyForm);
    }
  }

  updatePremium(): void {
    const gender = this.policyForm.get("client.gender")?.value;
    let premium = 0;

    if (gender === "F") {
      premium = 2500;
    } else if (gender === "M") {
      premium = 2000;
    }

    this.policyForm.get("amount")?.setValue(premium);
  }

  onlyNumbers(event: KeyboardEvent): boolean {
    const charCode = event.which ? event.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
      return false;
    }
    return true;
  }

  markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach((control) => {
      control.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }
}
