import { Component, OnInit } from "@angular/core";
import { ModalService } from "../../../services/modal/modal.service";

@Component({
  standalone: false,
  selector: "app-modal",
  templateUrl: "./modal.component.html",
  styleUrls: ["./modal.component.scss"],
})
export class ModalComponent implements OnInit {
  message: string = "";
  type: 'error' | 'success' | 'info' = 'info';
  isVisible: boolean = false;

  constructor(private modalService: ModalService) {}

  ngOnInit() {
    this.modalService.modalState$.subscribe(({ message, type }) => {
      this.message = message;
      this.type = type;
      this.isVisible = true;
    });
  }

  close() {
    this.isVisible = false;
  }
}