import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Policy } from '../../../interfaces/Policy';
import { AuthService } from '../../../services/auth/auth.service';
import { PolicyService } from '../../../services/policy/policy.service';
import { UserRole } from '../../../interfaces/Auth';
import { ModalService } from '../../../services/modal/modal.service';

@Component({
  standalone: false,
  selector: 'app-policycard',
  templateUrl: './policycard.component.html',
  styleUrls: ['./policycard.component.scss']
})
export class PolicycardComponent {
  @Input() policy!: Policy;
  @Output() statusChange = new EventEmitter<boolean>();

  constructor(
    private router: Router,
    private authService: AuthService,
    private policyService: PolicyService,
    private modalService: ModalService,

  ) {}

  getStatusClass(): string {
    switch (this.policy.status) {
      case 'Cotizada':
        return 'Cotizada';
      case 'Autorizada':
        return 'Autorizada';
      case 'Rechazada':
        return 'Rechazada';
      default:
        return '';
    }
  }

  viewPolicy(): void {
    this.router.navigate(['/policies/detail', this.policy.policyNumber]);
  }

  editPolicy(): void {
    this.router.navigate(['/policies/edit', this.policy.policyNumber]);
  }

  approvePolicy(): void {
    this.policyService.statusPolicy(this.policy.policyNumber,"Autorizada").subscribe({
      next:(data) =>{
        if(data) {
          this.modalService.show("Póliza autorizada correctamente","success");
          this.statusChange.emit(true);
        }
        else this.modalService.show("No se pudo autorizar la Póliza","error");
      },
      error:(err) =>{
        console.log(err);
        
         this.modalService.show( err,"error");
      }
    })
  }

  rejectPolicy(): void {
   this.policyService.statusPolicy(this.policy.policyNumber,"Rechazada").subscribe({
      next:(data) =>{
        if(data) {
          this.modalService.show("Póliza rechazada correctamente","success");
          this.statusChange.emit(true);
        }
        else this.modalService.show("No se pudo rechazar la Póliza","error");
      },
      error:(err) =>{
         this.modalService.show("Hubo un problema:" + err,"error");
      }
    })
  }

  canEdit(): boolean {
    return this.policy.status === 'Cotizada' && 
           (this.authService.getUserRole() === UserRole.ADMIN );
  }

  canApprove(): boolean {
    return this.policy.status === 'Cotizada' && 
           (this.authService.getUserRole() === UserRole.ADMIN || 
            this.authService.getUserRole() === UserRole.BROKER);
  }

  canReject(): boolean {
    return this.policy.status === 'Cotizada' && 
           (this.authService.getUserRole() === UserRole.ADMIN || 
            this.authService.getUserRole() === UserRole.BROKER);
  }
}
