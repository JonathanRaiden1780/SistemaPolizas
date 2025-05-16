import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { filter } from "rxjs/operators";

@Component({
  standalone: false,
  selector: "app-root",
  templateUrl: "app.component.html",
  styleUrls: ["app.component.scss"],
})
export class AppComponent implements OnInit {
  showHeader = false;

  constructor(private router: Router) {
    this.showHeader = false;
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe((event: any) => {
      this.showHeader = !event.url.includes("/auth/");
    });
  }

  ngOnInit(): void {}
}
