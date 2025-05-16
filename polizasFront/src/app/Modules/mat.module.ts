import { NgModule } from "@angular/core";
import { MatCardModule } from "@angular/material/card";
import { MatInputModule } from "@angular/material/input";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule} from "@angular/material/select";

@NgModule({
  imports: [MatCardModule,MatSelectModule, MatInputModule, MatButtonModule, MatIconModule, MatFormFieldModule],
  exports: [MatCardModule, MatSelectModule, MatInputModule, MatButtonModule, MatIconModule, MatFormFieldModule],
})
export class MatModule {}
