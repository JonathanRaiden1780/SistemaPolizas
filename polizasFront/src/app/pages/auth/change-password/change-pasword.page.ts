import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Location } from "@angular/common";
import { AuthService } from "../../../services/auth/auth.service";
import { ModalService } from "../../../services/modal/modal.service";

@Component({
  standalone: false,
  selector: "app-change-password",
  templateUrl: "./change-pasword.page.html",
  styleUrls: ["./change-pasword.page.scss"],
})
export class ChangePasswordPage {
  changePasswordForm: FormGroup;
  error = "";

  constructor(private fb: FormBuilder, private authService: AuthService, private location: Location, private modalService: ModalService) {
    this.changePasswordForm = this.fb.group({
      oldPassword: ["", Validators.required],
      newPassword: ["", Validators.required],
    });
  }

  onSubmit() {
    if (this.changePasswordForm.valid) {
      const { oldPassword, newPassword } = this.changePasswordForm.value;
      this.authService.changePassword({ oldPassword, newPassword }).subscribe({
        next: () => {
            this.modalService.show('Contraseña cambiada correctamente', 'success');
            this.location.back();
        },
        error: (error) => {
          this.error = "Error al cambiar la contraseña: ";
          this.modalService.show(this.error + error,'error');
        },
      });
    }
  }

  goBack() {
    this.location.back();
  }
}