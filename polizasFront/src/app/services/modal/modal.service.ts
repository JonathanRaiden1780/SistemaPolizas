import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ModalService {
  private modalSubject = new Subject<{ message: string; type: 'error' | 'success' | 'info' }>();
  modalState$ = this.modalSubject.asObservable();

  show(message: string, type: 'error' | 'success' | 'info') {
    this.modalSubject.next({ message, type });
  }
}